using System;
using System.Collections.Generic;

namespace MammothAPI.Data
{
    public partial class Logins
    {
        public Logins()
        {
            ProductGroups = new HashSet<ProductGroups>();
            ProductSales = new HashSet<ProductSales>();
            Products = new HashSet<Products>();
            Sales = new HashSet<Sales>();
            StoreGroups = new HashSet<StoreGroups>();
            Stores = new HashSet<Stores>();
            UsersLastModifiedByNavigation = new HashSet<Users>();
            UsersLogin = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ProductGroups> ProductGroups { get; set; }
        public virtual ICollection<ProductSales> ProductSales { get; set; }
        public virtual ICollection<Products> Products { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }
        public virtual ICollection<StoreGroups> StoreGroups { get; set; }
        public virtual ICollection<Stores> Stores { get; set; }
        public virtual ICollection<Users> UsersLastModifiedByNavigation { get; set; }
        public virtual ICollection<Users> UsersLogin { get; set; }
    }
}
