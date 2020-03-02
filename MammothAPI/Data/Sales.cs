using System;
using System.Collections.Generic;

namespace MammothAPI.Data
{
    public partial class Sales
    {
        public long Id { get; set; }
        public int StoreId { get; set; }
        public DateTime BusinessDate { get; set; }
        public int OnlineOrder { get; set; }
        public int OpenOrder { get; set; }
        public int PendingConsignment { get; set; }
        public int DamageQuantity { get; set; }
        public int PendingSapphireComplain { get; set; }
        public int MissingQuantity { get; set; }
        public int MissingCrate { get; set; }
        public int TotalOnline { get; set; }
        public int TotalMarquee { get; set; }
        public int PureMarquee { get; set; }
        public bool CashDeposite { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public int LastModifiedBy { get; set; }

        public virtual Logins LastModifiedByNavigation { get; set; }
        public virtual Stores Store { get; set; }
    }
}
