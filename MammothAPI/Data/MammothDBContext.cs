using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MammothAPI.Data
{
    public partial class MammothDBContext : DbContext
    {
        public MammothDBContext()
        {
        }

        public MammothDBContext(DbContextOptions<MammothDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<ProductGroups> ProductGroups { get; set; }
        public virtual DbSet<ProductSales> ProductSales { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<StoreGroups> StoreGroups { get; set; }
        public virtual DbSet<Stores> Stores { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MammothDB;Integrated Security=true;persist security info=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Logins>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.LoginsModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Logins_Users_Modified");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Logins_Stores");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LoginsUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Logins_Users");
            });

            modelBuilder.Entity<ProductGroups>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.ProductGroups)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductGroups_Users");
            });

            modelBuilder.Entity<ProductSales>(entity =>
            {
                entity.HasIndex(e => new { e.StoreId, e.BusinessDate })
                    .HasName("IX_ProductSales");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessDate).HasColumnType("date");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.ProductSalesModifiedByNavigation)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSales_Stores_Modified");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSales)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSales_Products");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ProductSalesStore)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSales_Stores");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Products_ProductGroups");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Users");
            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.HasIndex(e => new { e.StoreId, e.BusinessDate })
                    .HasName("IX_Sales");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BusinessDate).HasColumnType("date");

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.SalesLastModifiedByNavigation)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Stores_Modified");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.SalesStore)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Stores");
            });

            modelBuilder.Entity<StoreGroups>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Zone)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.StoreGroups)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreGroups_Users");
            });

            modelBuilder.Entity<Stores>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stores_Stores");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stores_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.LastModifiedByNavigation)
                    .WithMany(p => p.InverseLastModifiedByNavigation)
                    .HasForeignKey(d => d.LastModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
