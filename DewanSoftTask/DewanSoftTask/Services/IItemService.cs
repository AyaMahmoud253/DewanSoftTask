using DewanSoftTask.Models;

namespace DewanSoftTask.Services
{
    public interface IItemService
    {
        Task<Item> AddItemAsync(Item newItem);
    }

}
