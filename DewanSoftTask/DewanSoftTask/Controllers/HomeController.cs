using DewanSoftTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly ReceiptSystemContext _context;

    public HomeController(ReceiptSystemContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Fetch all items from the database
        var items = await _context.Items.ToListAsync();

        // Pass the list of items to the view
        return View(items);
    }
}
