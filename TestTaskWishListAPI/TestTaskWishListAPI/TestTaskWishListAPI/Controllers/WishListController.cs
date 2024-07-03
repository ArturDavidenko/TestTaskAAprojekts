using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestTaskWishListAPI.Dto;
using TestTaskWishListAPI.Models;
using TestTaskWishListAPI.Repository.Interfaces;

namespace TestTaskWishListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishListController : Controller
    {
        private readonly IWishListRepository _wishListRepository;
        private readonly IMapper _mapper;

        public WishListController(IWishListRepository wishListRepository, IMapper mapper) 
        {
            _wishListRepository = wishListRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WishItem>))]
        public IActionResult GetWishList()
        {
            var WishLists = _mapper.Map<List<WishItem>>(_wishListRepository.GetWishItems());
            return Ok(WishLists);
        }

        [HttpGet("{wishId}")]
        [ProducesResponseType(200, Type = typeof(WishItem))]
        [ProducesResponseType(400)]
        public IActionResult GetWishListItem(int wishId)
        {
            var wishItem = _mapper.Map<WishItem>(_wishListRepository.GetWishItem(wishId));
            if (wishItem == null)
                return BadRequest("This wish item not exist");
            else
                return Ok(wishItem);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateWishItem([FromBody] WishItem wishItemCreate)
        {
            var result = await _wishListRepository.CreateWishItemAsync(wishItemCreate);
            if (result.Success)
                return Ok();
            else
                return BadRequest(result.ErrorMessage);
        }

        [HttpPut("{wishItemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateWishItem(int wishItemId, [FromBody] WishItem updateWishItem)
        {
            var wishItemMap = _mapper.Map<WishItem>(updateWishItem);
            var result = await _wishListRepository.UpdateWishItemAsync(wishItemMap, wishItemId);
            if (result.Success)
                return Ok();
            else
                return BadRequest(result.ErrorMessage);
        }


        [HttpDelete("{wishItemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteWishItem(int wishItemId)
        {
            var wishItemToDelete = _wishListRepository.GetWishItem(wishItemId);
            var result = await _wishListRepository.DeleteWishItemAsync(wishItemToDelete);
            if (result.Success)
                return Ok();
            else
                return BadRequest(result.ErrorMessage);
        }

    }
}
