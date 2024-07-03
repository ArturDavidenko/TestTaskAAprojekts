using System.Text.Json.Serialization;

namespace TestTaskWishListAPP.Models
{
    public class WishItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("dateTimeAdd")]
        public DateTime DateTimeAdd { get; set; }
    }
}
