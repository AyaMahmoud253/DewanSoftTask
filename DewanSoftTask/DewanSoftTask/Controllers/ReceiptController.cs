using DewanSoftTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ReceiptController : Controller
{
    private readonly ReceiptSystemContext _context;

    public ReceiptController(ReceiptSystemContext context)
    {
        _context = context;
    }

    // GET: /Receipt/Index
    public IActionResult Index()
    {
        var receipts = _context.Receipts.Include(r => r.ReceiptItems).ThenInclude(ri => ri.Item).ToList();
        return View(receipts);
    }
    public async Task<IActionResult> Details(int id)
    {
        // Fetch the receipt and its associated items from the database
        var receipt = await _context.Receipts
            .Include(r => r.ReceiptItems)
            .ThenInclude(ri => ri.Item) // Include the associated items
            .FirstOrDefaultAsync(r => r.Id == id);

        if (receipt == null)
        {
            // Handle the case where the receipt is not found
            TempData["Error"] = "Receipt not found.";
            return RedirectToAction(nameof(Index));
        }

        return View(receipt);
    }

    // GET: Create Receipt
    public IActionResult Create()
    {
        var items = _context.Items.ToList();  // Fetch all available items
        return View(items);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(List<int> itemIds, List<int> quantities, decimal paidAmount)
    {
        if (itemIds == null || quantities == null || itemIds.Count != quantities.Count)
        {
            // Invalid input
            return View();
        }

        var receipt = new Receipt
        {
            Date = DateTime.Now,
            ReceiptItems = new List<ReceiptItem>(),
            PaidAmount = paidAmount
        };

        decimal totalAmount = 0;
        List<Item> updatedItems = new List<Item>(); // To store the updated items with their available stock

        for (int i = 0; i < itemIds.Count; i++)
        {
            var itemId = itemIds[i];
            var quantity = quantities[i];

            var item = await _context.Items.FindAsync(itemId);

            if (item == null || item.Balance < quantity)
            {
                // Show error if stock is not sufficient
                TempData["Error"] = $"Item {item?.Name} does not have enough stock.";
                return RedirectToAction(nameof(Create));
            }

            var receiptItem = new ReceiptItem
            {
                ItemId = itemId,
                Quantity = quantity,
                Item = item
            };
            receipt.ReceiptItems.Add(receiptItem);

            totalAmount += item.Price * quantity;

            // Deduct sold items from balance
            item.Balance -= quantity;

            // Track sold quantity
            item.AmountSold += quantity;

            // Add updated item to the list
            updatedItems.Add(item);
        }

        receipt.TotalAmount = totalAmount;
        receipt.RemainingAmount = totalAmount - paidAmount;

        _context.Receipts.Add(receipt);

        // Save changes to the database
        await _context.SaveChangesAsync();

        // Pass updated items to the view
        ViewData["UpdatedItems"] = updatedItems;

        return RedirectToAction(nameof(Index)); // Redirect to a page that shows the created receipt or receipts list
    }
    // GET: /Receipt/AddItem
    public IActionResult AddItem()
    {
        return View();
    }

    // POST: /Receipt/AddItem
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(Item newItem)
    {
        if (ModelState.IsValid)
        {
            _context.Items.Add(newItem); // Add the new item to the context
            await _context.SaveChangesAsync(); // Save changes to the database
            TempData["Success"] = "Item added successfully!";
            return RedirectToAction(nameof(Index)); // Redirect to the receipt index page or another page as needed
        }
        return View(newItem); // Return the form with errors if the model is invalid
    }

}
