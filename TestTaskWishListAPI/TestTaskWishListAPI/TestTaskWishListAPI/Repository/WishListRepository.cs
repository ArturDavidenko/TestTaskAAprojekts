using TestTaskWishListAPI.Data;
using TestTaskWishListAPI.Models;
using TestTaskWishListAPI.Repository.Interfaces;

namespace TestTaskWishListAPI.Repository
{
    public class WishListRepository : IWishListRepository
    {
        private readonly DBContext _context;

        public WishListRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Result> CreateWishItemAsync(WishItem wishItem)
        {
            var wishItems = GetWishItems()
                .Where(w => w.Title.Trim().ToUpper() == wishItem.Title.TrimEnd().ToUpper()).FirstOrDefault();

            if (wishItems != null)
                return Result.Fail("Wish item with this title already exists!");

            _context.Add(wishItem);
            if (await SaveAsync())
                return Result.Ok();
            else
                return Result.Fail("Error saving wish item.");
        }

        public async Task<Result> DeleteWishItemAsync(WishItem wishItem)
        {
            if (wishItem == null)
                return Result.Fail("Wish item id is not exist!");

            _context.Remove(wishItem);

            if (await SaveAsync())
                return Result.Ok();
            else
                return Result.Fail("Error saving wish item.");
        }

        public WishItem GetWishItem(int id)
        {
            if (!WishItemExists(id))
                return null;

            return _context.wishItems.Where(x => x.Id == id).FirstOrDefault(); //SingleOrDefault();
        }

        public WishItem GetWishItem(string title)
        {
            return _context.wishItems.Where(x => x.Title == title).FirstOrDefault();
        }

        public ICollection<WishItem> GetWishItems()
        {
            return _context.wishItems.OrderBy(x => x.Id).ToList(); //AsQueryable();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<Result> UpdateWishItemAsync(WishItem updateWishItem, int wishItemId)
        {
            if (wishItemId != updateWishItem.Id)
                return Result.Fail("Edit id and choosen id not equals!");

            if (updateWishItem == null)
                return Result.Fail("Wish item not exist!");

            if (!WishItemExists(wishItemId))
                return Result.Fail("Choosen wish item to edit not exist!");

            _context.Update(updateWishItem);

            if (await SaveAsync())
                return Result.Ok();
            else
                return Result.Fail("Error saving wish item.");
        }

        public bool WishItemExists(int id)
        {
            return _context.wishItems.Any(x => x.Id == id);
        }
    }
}
