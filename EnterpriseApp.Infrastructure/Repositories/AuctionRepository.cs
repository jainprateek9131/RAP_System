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
    public class AuctionRepository : IAuctionRepository
    {
        private readonly AppDbContext _context;

        public AuctionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAuctionAsync(Auction auction)
        {
            await _context.Auctions.AddAsync(auction);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Auction>> GetAllAuctionsAsync()
        {
            return await _context.Auctions.ToListAsync();
        }

        public async Task<Auction> GetAuctionByIdAsync(int id)
        {
            return await _context.Auctions
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAuctionAsync(Auction auction)
        {
            _context.Auctions.Update(auction);

            await _context.SaveChangesAsync();
        }
    }
}
