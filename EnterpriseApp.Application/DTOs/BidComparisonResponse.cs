using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Application.DTOs
{
    public class BidComparisonResponse
    {
        public int AuctionId { get; set; }

        public string AuctionTitle { get; set; }

        public decimal BasePrice { get; set; }

        public string Status { get; set; }

        public decimal? LowestBid { get; set; }

        public decimal? HighestBid { get; set; }

        public int TotalBids { get; set; }

        public int? WinnerVendorId { get; set; }

        public decimal? WinningBid { get; set; }

        public List<BidDetailsDto> Bids { get; set; }
    }

}
