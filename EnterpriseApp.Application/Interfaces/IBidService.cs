using EnterpriseApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Application.Interfaces
{
    public interface IBidService
    {
        Task<string> PlaceBidAsync(
            CreateBidRequest request,
            int vendorId
        );

        Task<List<BidResponse>> GetBidsByAuctionAsync(
            int auctionId
        );

        Task<string> UpdateBidAsync(
            int bidId,
            decimal amount,
            int vendorId
        );
        Task<BidComparisonResponse> GetBidComparisonDashboardAsync(
            int auctionId
        );
    }
}
