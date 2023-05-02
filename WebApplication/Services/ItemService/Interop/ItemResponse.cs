using Newtonsoft.Json;

namespace WebApplication.Services.ItemService.Interop
{
    public class ItemResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        public IList<ItemDto> Data { get; set; }

        public ItemResponse()
        {
            Data = new List<ItemDto>();
        }
    }
}
