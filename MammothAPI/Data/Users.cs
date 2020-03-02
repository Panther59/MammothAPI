using System;
using System.Collections.Generic;

namespace MammothAPI.Data
{
    public partial class Users
    {
        public Users()
        {
            Stores = new HashSet<Stores>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string LoginName { get; set; }
        public bool IsActive { get; set; }
        public int Type { get; set; }
        public int LoginId { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public int LastModifiedBy { get; set; }

        public virtual Logins LastModifiedByNavigation { get; set; }
        public virtual Logins Login { get; set; }
        public virtual ICollection<Stores> Stores { get; set; }
    }
}
