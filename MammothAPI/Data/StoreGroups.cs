using System;
using System.Collections.Generic;

namespace MammothAPI.Data
{
    public partial class StoreGroups
    {
        public StoreGroups()
        {
            Stores = new HashSet<Stores>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }

        public virtual Logins ModifiedByNavigation { get; set; }
        public virtual ICollection<Stores> Stores { get; set; }
    }
}
