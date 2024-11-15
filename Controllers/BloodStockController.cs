using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BloodStockController : Controller
{
    private readonly ApplicationDbContext _context;

    public BloodStockController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var bloodStocks = await _context.BloodStocks.ToListAsync();
        return View(bloodStocks);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateQuantity(int id, int quantityChange)
    {
        if (ModelState.IsValid)
        {
            var bloodStock = await _context.BloodStocks.FindAsync(id);
            if (bloodStock == null)
            {
                return NotFound();
            }

            bloodStock.QuantityAvailable += quantityChange;
            _context.Update(bloodStock);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View();
    }
}
