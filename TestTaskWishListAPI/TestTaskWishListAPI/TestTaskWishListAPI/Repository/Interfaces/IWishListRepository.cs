using TestTaskWishListAPI.Models;

namespace TestTaskWishListAPI.Repository.Interfaces
{
    public interface IWishListRepository
    {
        ICollection<WishItem> GetWishItems();

        WishItem GetWishItem(int id);

        WishItem GetWishItem(string title);

        bool WishItemExists(int id);

        Task<Result> CreateWishItemAsync(WishItem wishItem);

        Task<bool> SaveAsync();

        Task<Result> UpdateWishItemAsync(WishItem updateWishItem, int wishItemId);

        Task<Result> DeleteWishItemAsync(WishItem wishItem);
    }
}
