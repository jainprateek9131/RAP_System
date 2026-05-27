using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Application.DTOs
{
    public class BidDetailsDto
    {
        public int BidId { get; set; }

        public int VendorId { get; set; }

        public decimal BidAmount { get; set; }

        public DateTime BidTime { get; set; }
    }
}
