using DewanSoftTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace ReceiptSystem.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly ReceiptSystemContext _context;

        public ReceiptController(ReceiptSystemContext context)
        {
            _context = context;
        }

        // Create a new receipt
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                // Check if the balance is enough for each item in the receipt
                foreach (var item in receipt.ReceiptItems)
                {
                    var existingItem = _context.Items.FirstOrDefault(i => i.Id == item.ItemId);
                    if (existingItem == null || existingItem.Balance < item.Quantity)
                    {
                        ModelState.AddModelError("", $"Item {existingItem?.Name} does not have enough balance.");
                        return View(receipt);
                    }
                }

                // Deduct the item balance and update the amount sold
                foreach (var item in receipt.ReceiptItems)
                {
                    var existingItem = _context.Items.FirstOrDefault(i => i.Id == item.ItemId);
                    if (existingItem != null)
                    {
                        existingItem.Balance -= item.Quantity;
                        existingItem.AmountSold += item.Quantity;
                    }
                }

                // Save receipt
                _context.Receipts.Add(receipt);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(receipt);
        }
    }
}
