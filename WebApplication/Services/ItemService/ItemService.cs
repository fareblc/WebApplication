using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace WebApplication.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly RestClient _restClient =
            new(new Uri("https://cloudonapi.oncloud.gr/s1services/JS/updateItems/cloudOnTest"));

        public async Task<IList<ItemDto>> GetItemsAsync()
        {
            var responseDto = await _restClient.GetAsync<Interop.ItemResponse>(new RestRequest());

            if (responseDto == null)
            {
                return new List<ItemDto>();
            }

            var itemList = new List<ItemDto>();

            foreach (var item in responseDto.Data)
            {
                itemList.Add(new ItemDto
                {
                    Code = item.Code,
                    Id = Guid.NewGuid(),
                    Barcode = item.Barcode,
                    Discount = item.Discount,
                    ExternalId = item.ExternalId,
                    Description = item.Description,
                    RetailPrice = item.RetailPrice,
                    WholesalePrice = item.WholesalePrice,
                });
            }

            return itemList;
        }
    }
}
