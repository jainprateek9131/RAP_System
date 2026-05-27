using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Domain.Entities
{
    public class Bid
    {
        public int Id { get; set; }

        public int AuctionId { get; set; }

        public int VendorId { get; set; }

        public decimal BidAmount { get; set; }

        public DateTime BidTime { get; set; } = DateTime.UtcNow;
    }
}
