using Microsoft.AspNetCore.Mvc;
using mvc.Data;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var startOfWeek = GetStartOfWeek(DateTime.Now);
        var endOfWeek = startOfWeek.AddDays(7);

        var upcomingEvents = await _context.Events
            .Where(e => e.EventDate.Date >= startOfWeek.Date && e.EventDate.Date <= endOfWeek.Date)
            .OrderBy(e => e.EventDate)
            .ToListAsync();

        return View(upcomingEvents);
    }

    private DateTime GetStartOfWeek(DateTime date)
    {
        var diff = date.DayOfWeek - DayOfWeek.Monday;
        if (diff < 0) diff += 7;

        return date.AddDays(-diff).Date;
    }
}
