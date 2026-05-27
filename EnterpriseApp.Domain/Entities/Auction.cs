using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Domain.Entities
{
    public class Auction
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal BasePrice { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Status { get; set; } = "Active";

        public int CreatedBy { get; set; }

        public int? WinnerVendorId { get; set; }

        public decimal? WinningBid { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
