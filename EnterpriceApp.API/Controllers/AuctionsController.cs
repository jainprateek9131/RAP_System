using EnterpriseApp.Application.DTOs;
using EnterpriseApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnterpriceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : BaseController
    {
        private readonly IAuctionService _auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        // CREATE AUCTION

        [Authorize(Roles = "Buyer")]
        [HttpPost]
        public async Task<IActionResult> CreateAuction(
            CreateAuctionRequest request)
        {
            int buyerId = Convert.ToInt32(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            var result = await _auctionService
                .CreateAuctionAsync(request, buyerId);

            return Success(
                result,
                201,
                "Auction Created Successfully"
            );
        }

        // GET ALL AUCTIONS

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAuctions()
        {
            var result = await _auctionService
                .GetAllAuctionsAsync();

            return Success(
                result,
                "Auctions Fetched Successfully"
            );
        }

        // GET AUCTION BY ID

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuctionById(int id)
        {
            var result = await _auctionService
                .GetAuctionByIdAsync(id);

            if (result == null)
            {
                return NotFoundResponse(
                    "Auction Not Found"
                );
            }

            return Success(
                result,
                "Auction Fetched Successfully"
            );
        }

        // CLOSE AUCTION

        [Authorize(Roles = "Buyer")]
        [HttpPut("close/{id}")]
        public async Task<IActionResult> CloseAuction(int id)
        {
            var result = await _auctionService
                .CloseAuctionAsync(id);

            return Success(
                result,
                "Auction Closed Successfully"
            );
        }
    }
}
