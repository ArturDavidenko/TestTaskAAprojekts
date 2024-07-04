using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestTaskWishListAPP.Models;
using TestTaskWishListAPP.Services;

namespace TestTaskWishListAPP.Controllers
{
    public class HomeController : Controller
    {

        private readonly WishItemsService _service;

        public HomeController(WishItemsService repository)
        {
            _service = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> HomePage() 
        {
            var wishItems = await _service.GetWishItems();
            return View(wishItems);
        }

        public async Task<IActionResult> DeleteWishItem(int id)
        {
            await _service.DeleteWishItem(id);
            return RedirectToAction("HomePage");
        }

        public async Task<IActionResult> CreateWishItem(string title, string description)
        {
            await _service.CreateWishItem(title, description);
            return RedirectToAction("HomePage");
        }

        [HttpPost]
        public async Task<IActionResult> EditWishItem([FromBody] WishItem item)
        {
            await _service.UpdateWishItem(item);
            return RedirectToAction("HomePage");
        }


    }       
}
