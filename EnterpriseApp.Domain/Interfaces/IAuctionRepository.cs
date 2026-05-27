using EnterpriseApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Domain.Interfaces
{
    public interface IAuctionRepository
    {
        Task AddAuctionAsync(Auction auction);

        Task<List<Auction>> GetAllAuctionsAsync();

        Task<Auction> GetAuctionByIdAsync(int id);

        Task UpdateAuctionAsync(Auction auction);
    }
}
