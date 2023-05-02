namespace WebApplication.Services.ItemService
{
    public interface IItemService
    {
        Task<IList<ItemDto>> GetItemsAsync();
    }
}
