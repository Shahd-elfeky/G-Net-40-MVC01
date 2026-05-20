using Microsoft.AspNetCore.Mvc;
using MVCProject01.Data;
using Microsoft.EntityFrameworkCore;

namespace MVCProject01.Controllers;

public class PlanController : Controller
{
    private readonly GymDbContext _context;

    public PlanController(GymDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var plans = await _context.Plans
            .Where(plan => plan.IsActive)
            .ToListAsync();

        return View(plans);
    }

    public async Task<IActionResult> Details(int id)
    {
        var plan = await _context.Plans.FindAsync(id);

        if (plan is null)
        {
            return NotFound();
        }

        return View(plan);
    }
}
