using DewanSoftTask.Models;

namespace DewanSoftTask.Services
{
    public interface IReceiptService
    {
        Task<List<Receipt>> GetAllReceiptsAsync();
        Task<Receipt> GetReceiptByIdAsync(int id);
        Task<Receipt> CreateReceiptAsync(List<int> itemIds, List<int> quantities, decimal paidAmount);
        Task<List<Item>> GetAllItemsAsync();
    }

}
