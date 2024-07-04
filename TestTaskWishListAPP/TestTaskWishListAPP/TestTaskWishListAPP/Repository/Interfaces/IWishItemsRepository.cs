using TestTaskWishListAPP.Models;

namespace TestTaskWishListAPP.Services.Interfaces
{
    public interface IWishItemsRepository
    {
        Task<List<WishItem>> GetWishItems();

        public Task DeleteWishItem(int id);

        public Task CreateWishItem(string title, string description);

        public Task UpdateWishItem(WishItem item);
    }
}
