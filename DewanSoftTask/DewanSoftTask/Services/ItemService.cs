using DewanSoftTask.Models;

namespace DewanSoftTask.Services
{
    public class ItemService : IItemService
    {
        private readonly ReceiptSystemContext _context;

        public ItemService(ReceiptSystemContext context)
        {
            _context = context;
        }

        public async Task<Item> AddItemAsync(Item newItem)
        {
            if (newItem == null)
            {
                throw new ArgumentNullException(nameof(newItem), "Item cannot be null.");
            }

            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();
            return newItem;
        }
    }

}
