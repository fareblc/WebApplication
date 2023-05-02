using Newtonsoft.Json;

namespace WebApplication.Services.ItemService.Interop
{
    public class ItemDto
    {
        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("retailPrice")]
        public string RetailPrice { get; set; }

        [JsonProperty("wholesalePrice")]
        public string WholesalePrice { get; set; }

        [JsonProperty("discount")]
        public string Discount { get; set; }
    }
}
