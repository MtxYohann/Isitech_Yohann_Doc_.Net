using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class EventController : Controller
{
    private readonly ApplicationDbContext _contexts;

    private readonly UserManager<Teacher> _userManager;


    public EventController(ApplicationDbContext context, UserManager<Teacher> userManager)
    {
        _contexts = context;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index(string searchTitle, DateTime? searchDate)
    {
        var eventsQuery = _contexts.Events.AsQueryable();

        if (!string.IsNullOrEmpty(searchTitle))
        {
            eventsQuery = eventsQuery.Where(e => e.Title.Contains(searchTitle));
        }

        if (searchDate.HasValue)
        {
            eventsQuery = eventsQuery.Where(e => e.EventDate.Date == searchDate.Value.Date);
        }

        var events = await eventsQuery.ToListAsync();

        ViewData["searchTitle"] = searchTitle;
        ViewData["searchDate"] = searchDate?.ToString("yyyy-MM-dd");

        return View(events);
    }


    [HttpGet]
    [Authorize]
    public async Task<ActionResult> Update(int id)
    {

        var events = await _contexts.Events.FirstOrDefaultAsync(e => e.Id == id);
        if (events == null)
        {
            return NotFound();
        }
        var model = new Event
        {
            Title = events.Title,
            Description = events.Description,
            EventDate = events.EventDate,
            MaxParticipants = events.MaxParticipants,
            Location = events.Location
        };
        return View(model);
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Update(Event model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Error when editing";
            return View(model);
        }
        var events = await _contexts.Events.FirstOrDefaultAsync(e => e.Id == model.Id);
        if (events == null) return NotFound("Event not found");
        events.Title = model.Title;
        events.Description = model.Description;
        events.EventDate = model.EventDate;
        events.MaxParticipants = model.MaxParticipants;
        events.Location = model.Location;

        await _contexts.SaveChangesAsync();

        return RedirectToAction("Index", "Event");

    }

    [Authorize]
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Add(Event model)
    {

        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Error when creating the event";
            return View(model);
        }

        await _contexts.AddAsync(model);

        await _contexts.SaveChangesAsync();

        return View("Index");

    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var events = await _contexts.Events.FirstOrDefaultAsync(e => e.Id == id);
        if (events == null)
        {
            return NotFound();
        }
        return View(events);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> DeleteConfirmed(Event model)
    {
        var events = await _contexts.Events.FirstOrDefaultAsync(e => e.Id == model.Id);
        if (events == null)
        {
            TempData["ErrorMessage"] = "Event not found.";
            return RedirectToAction(nameof(Index));
        }

        _contexts.Events.Remove(events);
        await _contexts.SaveChangesAsync();

        TempData["SuccessMessage"] = "Event deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

}

