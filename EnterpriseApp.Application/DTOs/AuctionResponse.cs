using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Application.DTOs
{
    public class AuctionResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal BasePrice { get; set; }

        public string Status { get; set; }

        public DateTime EndTime { get; set; }
    }
}
