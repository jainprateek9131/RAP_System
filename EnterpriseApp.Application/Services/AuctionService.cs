using EnterpriseApp.Application.DTOs;
using EnterpriseApp.Application.Interfaces;
using EnterpriseApp.Domain.Entities;
using EnterpriseApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Application.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepository;

        public AuctionService(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task<string> CreateAuctionAsync(
            CreateAuctionRequest request,
            int buyerId)
        {
            var auction = new Auction
            {
                Title = request.Title,
                Description = request.Description,
                BasePrice = request.BasePrice,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                CreatedBy = buyerId
            };

            await _auctionRepository.AddAuctionAsync(auction);

            return "Auction Created Successfully";
        }

        public async Task<List<AuctionResponse>> GetAllAuctionsAsync()
        {
            var auctions = await _auctionRepository
                .GetAllAuctionsAsync();

            return auctions.Select(x => new AuctionResponse
            {
                Id = x.Id,
                Title = x.Title,
                BasePrice = x.BasePrice,
                Status = x.Status,
                EndTime = x.EndTime
            }).ToList();
        }

        public async Task<AuctionResponse> GetAuctionByIdAsync(int id)
        {
            var auction = await _auctionRepository
                .GetAuctionByIdAsync(id);

            if (auction == null)
            {
                return null;
            }

            return new AuctionResponse
            {
                Id = auction.Id,
                Title = auction.Title,
                BasePrice = auction.BasePrice,
                Status = auction.Status,
                EndTime = auction.EndTime
            };
        }

        public async Task<string> CloseAuctionAsync(int auctionId)
        {
            var auction = await _auctionRepository
                .GetAuctionByIdAsync(auctionId);

            if (auction == null)
            {
                return "Auction Not Found";
            }

            auction.Status = "Closed";

            await _auctionRepository.UpdateAuctionAsync(auction);

            return "Auction Closed Successfully";
        }
    }
}
