using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MammothAPI.Models
{
	public class StoreSale
	{
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
    }
}
