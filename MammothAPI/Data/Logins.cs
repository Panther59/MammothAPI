using System;
using System.Collections.Generic;

namespace MammothAPI.Data
{
    public partial class Logins
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? StoreId { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }

        public virtual Users ModifiedByNavigation { get; set; }
        public virtual Stores Store { get; set; }
        public virtual Users User { get; set; }
    }
}
