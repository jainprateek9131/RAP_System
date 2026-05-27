using EnterpriseApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Domain.Interfaces
{
    public interface IBidRepository
    {
        Task AddBidAsync(Bid bid);

        Task<List<Bid>> GetBidsByAuctionIdAsync(int auctionId);

        Task<decimal?> GetLowestBidAsync(int auctionId);

        Task<Bid> GetBidByIdAsync(int bidId);

        Task UpdateBidAsync(Bid bid);
        Task<decimal?> GetHighestBidAsync(int auctionId);

        Task<int> GetTotalBidsAsync(int auctionId);
    }
}
