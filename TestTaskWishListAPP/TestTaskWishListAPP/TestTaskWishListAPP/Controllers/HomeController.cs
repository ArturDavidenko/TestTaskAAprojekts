using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestTaskWishListAPP.Models;
using TestTaskWishListAPP.Services.Repository;


namespace TestTaskWishListAPP.Controllers
{
    public class HomeController : Controller
    {

        private readonly WishItemsRepository _repository;

        public HomeController(WishItemsRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> HomePage() 
        {
            var wishItems = await _repository.GetWishItems();
            return View(wishItems);
        }

        public async Task<IActionResult> DeleteWishItem(int id)
        {
            await _repository.DeleteWishItem(id);
            return RedirectToAction("HomePage");
        }

        public async Task<IActionResult> CreateWishItem(string title, string description)
        {
            await _repository.CreateWishItem(title, description);
            return RedirectToAction("HomePage");
        }

        [HttpPost]
        public async Task<IActionResult> EditWishItem([FromBody] WishItem item)
        {
            await _repository.UpdateWishItem(item);
            return RedirectToAction("HomePage");
        }


    }       
}
