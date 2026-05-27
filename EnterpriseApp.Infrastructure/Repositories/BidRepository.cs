using EnterpriseApp.Domain.Entities;
using EnterpriseApp.Domain.Interfaces;
using EnterpriseApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseApp.Infrastructure.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly AppDbContext _context;

        public BidRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddBidAsync(Bid bid)
        {
            await _context.Bids.AddAsync(bid);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Bid>> GetBidsByAuctionIdAsync(int auctionId)
        {
            return await _context.Bids
                .Where(x => x.AuctionId == auctionId)
                .OrderBy(x => x.BidAmount)
                .ToListAsync();
        }

        public async Task<decimal?> GetLowestBidAsync(int auctionId)
        {
            return await _context.Bids
                .Where(x => x.AuctionId == auctionId)
                .MinAsync(x => (decimal?)x.BidAmount);
        }

        public async Task<Bid> GetBidByIdAsync(int bidId)
        {
            return await _context.Bids
                .FirstOrDefaultAsync(x => x.Id == bidId);
        }

        public async Task UpdateBidAsync(Bid bid)
        {
            _context.Bids.Update(bid);

            await _context.SaveChangesAsync();
        }
        public async Task<decimal?> GetHighestBidAsync(int auctionId)
        {
            return await _context.Bids
                .Where(x => x.AuctionId == auctionId)
                .MaxAsync(x => (decimal?)x.BidAmount);
        }

        public async Task<int> GetTotalBidsAsync(int auctionId)
        {
            return await _context.Bids
                .CountAsync(x => x.AuctionId == auctionId);
        }
    }
}
