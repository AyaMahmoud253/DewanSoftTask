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

            // Validate that Price is positive
            if (newItem.Price <= 0)
            {
                throw new ArgumentException("Price must be a positive value.");
            }

            // Validate that Balance is positive
            if (newItem.Balance < 0)
            {
                throw new ArgumentException("Balance cannot be negative.");
            }

            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();
            return newItem;
        }

    }

}
