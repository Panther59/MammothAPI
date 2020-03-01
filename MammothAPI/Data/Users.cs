using System;
using System.Collections.Generic;

namespace MammothAPI.Data
{
    public partial class Users
    {
        public Users()
        {
            InverseLastModifiedByNavigation = new HashSet<Users>();
            LoginsModifiedByNavigation = new HashSet<Logins>();
            LoginsUser = new HashSet<Logins>();
            ProductGroups = new HashSet<ProductGroups>();
            Products = new HashSet<Products>();
            StoreGroups = new HashSet<StoreGroups>();
            Stores = new HashSet<Stores>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string LoginName { get; set; }
        public bool IsActive { get; set; }
        public int Type { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public int LastModifiedBy { get; set; }

        public virtual Users LastModifiedByNavigation { get; set; }
        public virtual ICollection<Users> InverseLastModifiedByNavigation { get; set; }
        public virtual ICollection<Logins> LoginsModifiedByNavigation { get; set; }
        public virtual ICollection<Logins> LoginsUser { get; set; }
        public virtual ICollection<ProductGroups> ProductGroups { get; set; }
        public virtual ICollection<Products> Products { get; set; }
        public virtual ICollection<StoreGroups> StoreGroups { get; set; }
        public virtual ICollection<Stores> Stores { get; set; }
    }
}
