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
        if (ModelState.IsValid)
        {
            try
            {
                var addedItem = await _itemService.AddItemAsync(newItem); // Call service to add the item
                TempData["Success"] = "Item added successfully!";
                return RedirectToAction("Index", "Home"); 
            }
            catch (ArgumentNullException ex)
            {
                TempData["Error"] = ex.Message;
                return View(newItem); // Return the form with error messages
            }
        }
        return View(newItem); // Return the form with validation errors
    }
}
