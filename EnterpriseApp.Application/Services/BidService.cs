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
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        private readonly IAuctionRepository _auctionRepository;

        public BidService(
            IBidRepository bidRepository,
            IAuctionRepository auctionRepository)
        {
            _bidRepository = bidRepository;
            _auctionRepository = auctionRepository;
        }

        // PLACE BID

        public async Task<string> PlaceBidAsync(
            CreateBidRequest request,
            int vendorId)
        {
            var auction = await _auctionRepository
                .GetAuctionByIdAsync(request.AuctionId);

            if (auction == null)
            {
                return "Auction Not Found";
            }

            if (auction.Status == "Closed")
            {
                return "Auction Already Closed";
            }

            var currentLowestBid = await _bidRepository
                .GetLowestBidAsync(request.AuctionId);

            // FIRST BID

            if (currentLowestBid == null)
            {
                if (request.BidAmount >= auction.BasePrice)
                {
                    return "Bid must be lower than base price";
                }
            }
            else
            {
                if (request.BidAmount >= currentLowestBid)
                {
                    return "Bid must be lower than current lowest bid";
                }
            }

            var bid = new Bid
            {
                AuctionId = request.AuctionId,
                VendorId = vendorId,
                BidAmount = request.BidAmount
            };

            await _bidRepository.AddBidAsync(bid);

            return "Bid Placed Successfully";
        }

        // GET BIDS

        public async Task<List<BidResponse>> GetBidsByAuctionAsync(
            int auctionId)
        {
            var bids = await _bidRepository
                .GetBidsByAuctionIdAsync(auctionId);

            return bids.Select(x => new BidResponse
            {
                Id = x.Id,
                AuctionId = x.AuctionId,
                VendorId = x.VendorId,
                BidAmount = x.BidAmount,
                BidTime = x.BidTime
            }).ToList();
        }

        // UPDATE BID

        public async Task<string> UpdateBidAsync(
            int bidId,
            decimal amount,
            int vendorId)
        {
            var bid = await _bidRepository
                .GetBidByIdAsync(bidId);

            if (bid == null)
            {
                return "Bid Not Found";
            }

            if (bid.VendorId != vendorId)
            {
                return "You cannot update another vendor bid";
            }

            var currentLowestBid = await _bidRepository
                .GetLowestBidAsync(bid.AuctionId);

            if (amount >= currentLowestBid)
            {
                return "Updated amount must be lower than current lowest bid";
            }

            bid.BidAmount = amount;

            bid.BidTime = DateTime.UtcNow;

            await _bidRepository.UpdateBidAsync(bid);

            return "Bid Updated Successfully";
        }

        public async Task<BidComparisonResponse> GetBidComparisonDashboardAsync(int auctionId)
        {
            var auction = await _auctionRepository
                .GetAuctionByIdAsync(auctionId);

            if (auction == null)
            {
                return null;
            }

            var bids = await _bidRepository
                .GetBidsByAuctionIdAsync(auctionId);

            var lowestBid = await _bidRepository
                .GetLowestBidAsync(auctionId);

            var highestBid = await _bidRepository
                .GetHighestBidAsync(auctionId);

            var totalBids = await _bidRepository
                .GetTotalBidsAsync(auctionId);

            return new BidComparisonResponse
            {
                AuctionId = auction.Id,

                AuctionTitle = auction.Title,

                BasePrice = auction.BasePrice,

                Status = auction.Status,

                LowestBid = lowestBid,

                HighestBid = highestBid,

                TotalBids = totalBids,

                WinnerVendorId = auction.WinnerVendorId,

                WinningBid = auction.WinningBid,

                Bids = bids.Select(x => new BidDetailsDto
                {
                    BidId = x.Id,
                    VendorId = x.VendorId,
                    BidAmount = x.BidAmount,
                    BidTime = x.BidTime
                }).ToList()
            };
        }
    }
}
