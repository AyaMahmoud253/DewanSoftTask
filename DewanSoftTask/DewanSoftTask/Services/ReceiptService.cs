using DewanSoftTask.Models;
using Microsoft.EntityFrameworkCore;

namespace DewanSoftTask.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly ReceiptSystemContext _context;

        public ReceiptService(ReceiptSystemContext context)
        {
            _context = context;
        }

        public async Task<List<Receipt>> GetAllReceiptsAsync()
        {
            return await _context.Receipts.Include(r => r.ReceiptItems)
                                          .ThenInclude(ri => ri.Item)
                                          .ToListAsync();
        }

        public async Task<Receipt> GetReceiptByIdAsync(int id)
        {
            var receipt = await _context.Receipts
                                        .Include(r => r.ReceiptItems)
                                        .ThenInclude(ri => ri.Item)
                                        .FirstOrDefaultAsync(r => r.Id == id);

            return receipt;
        }

        public async Task<Receipt> CreateReceiptAsync(List<int> itemIds, List<int> quantities, decimal paidAmount)
        {
            if (itemIds == null || quantities == null || itemIds.Count == 0 || quantities.Count == 0)
                throw new ArgumentException("Please select at least one item and fill all fields to proceed.");

            if (itemIds.Count != quantities.Count)
                throw new ArgumentException("Mismatch between items and quantities.");

            var receipt = new Receipt
            {
                Date = DateTime.Now,
                ReceiptItems = new List<ReceiptItem>(),
                PaidAmount = paidAmount
            };

            decimal totalAmount = 0;
            var updatedItems = new List<Item>();

            for (int i = 0; i < itemIds.Count; i++)
            {
                var item = await _context.Items.FindAsync(itemIds[i]);

                if (item == null || item.Balance < quantities[i])
                    throw new ArgumentException($"Item {item?.Name} does not have enough stock.");

                totalAmount += item.Price * quantities[i];

                var receiptItem = new ReceiptItem
                {
                    ItemId = itemIds[i],
                    Quantity = quantities[i],
                    Item = item
                };
                receipt.ReceiptItems.Add(receiptItem);

                item.Balance -= quantities[i];
                item.AmountSold += quantities[i];
                updatedItems.Add(item);
            }

            receipt.TotalAmount = totalAmount;
            receipt.RemainingAmount = totalAmount - paidAmount;

            _context.Receipts.Add(receipt);
            await _context.SaveChangesAsync();

            return receipt;
        }
        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();  // Fetch all available items from the database
        }
    }
}
