using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BloodStockController : Controller
{
    private readonly ApplicationDbContext _context;

    public BloodStockController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: BloodStock/Index
    public async Task<IActionResult> Index()
    {
        // Display the current blood stock in the database
        var bloodStocks = await _context.BloodStocks.ToListAsync();
        return View(bloodStocks);
    }

    // POST: BloodStock/UpdateQuantity
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateQuantity(int id, int quantityChange)
    {
        if (ModelState.IsValid)
        {
            // Find the selected blood stock record
            var bloodStock = await _context.BloodStocks.FindAsync(id);
            if (bloodStock == null)
            {
                return NotFound();
            }

            // Update the quantity (add or subtract based on quantityChange)
            bloodStock.QuantityAvailable += quantityChange;
            _context.Update(bloodStock);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View();
    }
}
