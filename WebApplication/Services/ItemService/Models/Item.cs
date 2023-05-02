namespace WebApplication.Services.ItemService.Models
{
    public class Item
    {
        public string ExternalId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public string RetailPrice { get; set; }
        public string WholesalePrice { get; set; }
        public string Discount { get; set; }
    }
}
