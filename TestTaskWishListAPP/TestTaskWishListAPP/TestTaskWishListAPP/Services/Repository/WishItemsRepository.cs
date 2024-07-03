using System.Text;
using System.Text.Json;
using TestTaskWishListAPP.Models;
using TestTaskWishListAPP.Services.Interfaces;

namespace TestTaskWishListAPP.Services.Repository
{
    public class WishItemsRepository : IWishItemsRepository
    {
        private readonly string _RequesURL = "https://localhost:7249/api/WishList"; //Write your local url request from API

        public async Task CreateWishItem(string title, string description)
        {
            var wishItem = new WishItem { Title = title, Description = description };

            using (HttpClient httpClient = new HttpClient())
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(wishItem), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(_RequesURL, jsonContent);
            }
        }

        public async Task DeleteWishItem(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"{_RequesURL}/{id}");
            }
        }

        public async Task<List<WishItem>> GetWishItems()
        {
            List<WishItem> wishItems = new List<WishItem>();

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(_RequesURL);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    wishItems = JsonSerializer.Deserialize<List<WishItem>>(jsonResponse);

                }
            }
            return wishItems;
        }

        public async Task UpdateWishItem(WishItem item)
        {
            WishItem currentItem = null;
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{_RequesURL}/{item.Id}");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    currentItem = JsonSerializer.Deserialize<WishItem>(jsonResponse);
                }

            }

            if (item.Title != null)
            {
                currentItem.Title = item.Title;
            }

            if (item.Description != null)
            {
                currentItem.Description = item.Description;
            }

            using (HttpClient httpClient = new HttpClient())
            {
                var json = JsonSerializer.Serialize(currentItem);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"{_RequesURL}/{currentItem.Id}", content);
            }
        }
    }
}
