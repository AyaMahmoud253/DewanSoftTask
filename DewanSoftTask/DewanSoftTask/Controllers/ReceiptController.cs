using DewanSoftTask.Services;
using Microsoft.AspNetCore.Mvc;

public class ReceiptController : Controller
{
    private readonly IReceiptService _receiptService;

    public ReceiptController(IReceiptService receiptService)
    {
        _receiptService = receiptService;
    }

    // GET: /Receipt/Index
    public async Task<IActionResult> Index()
    {
        var receipts = await _receiptService.GetAllReceiptsAsync();
        return View(receipts);
    }

    public async Task<IActionResult> Details(int id)
    {
        var receipt = await _receiptService.GetReceiptByIdAsync(id);

        if (receipt == null)
        {
            TempData["Error"] = "Receipt not found.";
            return RedirectToAction(nameof(Index));
        }

        return View(receipt);
    }

    // GET: Create Receipt
    public async Task<IActionResult> Create()
    {
        var items = await _receiptService.GetAllItemsAsync();  // Fetch all available items
        return View(items);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(List<int> itemIds, List<int> quantities, decimal paidAmount)
    {
        try
        {
            var receipt = await _receiptService.CreateReceiptAsync(itemIds, quantities, paidAmount);
            return RedirectToAction(nameof(Index));  // Or you could redirect to Details(receipt.Id)
        }
        catch (ArgumentException ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction(nameof(Create));
        }
    }
}
