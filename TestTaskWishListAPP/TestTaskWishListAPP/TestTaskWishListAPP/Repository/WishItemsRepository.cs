using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using TestTaskWishListAPP.Models;
using TestTaskWishListAPP.Services.Interfaces;

namespace TestTaskWishListAPP.Services
{
    public class WishItemsRepository : IWishItemsRepository
    {
        private readonly string _requesURL;

        public WishItemsRepository(IOptions<ApiSetting> apiSetting)
        {
            _requesURL = apiSetting.Value.RequesURL;
        }

        public async Task CreateWishItem(string title, string description)
        {
            var wishItem = new WishItem { Title = title, Description = description };

            using (HttpClient httpClient = new HttpClient())
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(wishItem), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(_requesURL, jsonContent);
            }
        }

        public async Task DeleteWishItem(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"{_requesURL}/{id}");
            }
        }

        public async Task<List<WishItem>> GetWishItems()
        {
            List<WishItem> wishItems = new List<WishItem>();

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(_requesURL);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    wishItems = JsonSerializer.Deserialize<List<WishItem>>(jsonResponse);

                }
            }
            return wishItems.ToList();
        }

        public async Task UpdateWishItem(WishItem item)
        {
            WishItem currentItem = null;
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{_requesURL}/{item.Id}");

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
                var response = await httpClient.PutAsync($"{_requesURL}", content);
            }
        }
    }
}
