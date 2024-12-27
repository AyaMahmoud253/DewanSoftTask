using DewanSoftTask.Models;
using DewanSoftTask.Services;
using Microsoft.AspNetCore.Mvc;

public class ItemController : Controller
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    // GET: /Item/AddItem
    public IActionResult AddItem()
    {
        return View();
    }

    // POST: /Item/AddItem
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(Item newItem)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Please correct the errors in the form.";
            return View(newItem);
        }

        try
        {
            // Additional server-side validation
            if (newItem.Price <= 0)
            {
                TempData["Error"] = "Price must be a positive value.";
                return View(newItem);
            }

            if (newItem.Balance < 0)
            {
                TempData["Error"] = "Balance cannot be negative.";
                return View(newItem);
            }

            var addedItem = await _itemService.AddItemAsync(newItem); // Call service to add the item
            TempData["Success"] = "Item added successfully!";
            return RedirectToAction("Index", "Home");
        }
        catch (ArgumentException ex)
        {
            TempData["Error"] = ex.Message;
            return View(newItem); // Return the form with error messages
        }
        catch (Exception ex)
        {
            // Handle unexpected errors
            TempData["Error"] = "An unexpected error occurred. Please try again.";
            // Log the error (optional)
            Console.WriteLine($"Error: {ex.Message}");
            return View(newItem);
        }
    }
}
