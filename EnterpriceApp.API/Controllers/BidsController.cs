using EnterpriseApp.Application.DTOs;
using EnterpriseApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnterpriceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : BaseController
    {
        private readonly IBidService _bidService;

        public BidsController(IBidService bidService)
        {
            _bidService = bidService;
        }

        // PLACE BID

        [Authorize(Roles = "Vendor")]
        [HttpPost]
        public async Task<IActionResult> PlaceBid(
            CreateBidRequest request)
        {
            int vendorId = Convert.ToInt32(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            var result = await _bidService
                .PlaceBidAsync(request, vendorId);

            return Success(
                result,
                201,
                "Bid Placed Successfully"
            );
        }

        // GET BIDS BY AUCTION

        [Authorize]
        [HttpGet("auction/{auctionId}")]
        public async Task<IActionResult> GetBidsByAuction(
            int auctionId)
        {
            var result = await _bidService
                .GetBidsByAuctionAsync(auctionId);

            return Success(
                result,
                "Bids Fetched Successfully"
            );
        }

        // UPDATE BID

        [Authorize(Roles = "Vendor")]
        [HttpPut("{bidId}")]
        public async Task<IActionResult> UpdateBid(
            int bidId,
            decimal amount)
        {
            int vendorId = Convert.ToInt32(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            var result = await _bidService
                .UpdateBidAsync(
                    bidId,
                    amount,
                    vendorId
                );

            return Success(
                result,
                "Bid Updated Successfully"
            );
        }

        // BID DASHBOARD

        [Authorize]
        [HttpGet("dashboard/{auctionId}")]
        public async Task<IActionResult> GetBidDashboard(
            int auctionId)
        {
            var result = await _bidService
                .GetBidComparisonDashboardAsync(auctionId);

            if (result == null)
            {
                return NotFoundResponse(
                    "Auction Not Found"
                );
            }

            return Success(
                result,
                "Dashboard Fetched Successfully"
            );
        }
    }
}
