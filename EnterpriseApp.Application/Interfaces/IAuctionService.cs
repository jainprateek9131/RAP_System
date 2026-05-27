using EnterpriseApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Application.Interfaces
{
    public interface IAuctionService
    {
        Task<string> CreateAuctionAsync(
            CreateAuctionRequest request,
            int buyerId
        );

        Task<List<AuctionResponse>> GetAllAuctionsAsync();

        Task<AuctionResponse> GetAuctionByIdAsync(int id);

        Task<string> CloseAuctionAsync(int auctionId);
    }
}
