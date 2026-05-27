using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Application.DTOs
{
    public class CreateBidRequest
    {
        public int AuctionId { get; set; }

        public decimal BidAmount { get; set; }
    }
}
