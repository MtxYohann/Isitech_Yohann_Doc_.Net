using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class EventController : Controller
{
    private readonly ApplicationDbContext _contexts;

    private readonly UserManager<User> _userManager;


    public EventController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _contexts = context;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index(string searchTitle, DateTime? searchDate)
    {
        var eventsQuery = _contexts.Events.AsQueryable();
        
        eventsQuery = eventsQuery.Include(e => e.UserEvents);

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
    [Authorize(Roles = "Teacher")]
    public async Task<ActionResult> Update(int id, bool confirm = false)
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
        ViewData["ConfirmUpdate"] = confirm;
        return View(model);
    }
    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public async Task<ActionResult> Update(Event model, bool confirm = false)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Error when editing";
            return View(model);
        }
        if (!confirm)
        {
            ViewData["ConfirmUpdate"] = true;
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

    [Authorize(Roles = "Teacher")]
    [HttpGet]
    public IActionResult Add(bool confirm = false)
    {
        ViewData["ConfirmAdd"] = confirm;
        return View();
    }
    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<IActionResult> Add(Event model, bool confirm = false)
    {

        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Error when creating the event.";
            return View(model);
        }

        if (!confirm)
        {
            ViewData["ConfirmAdd"] = true;
            return View(model);
        }
        await _contexts.AddAsync(model);
        await _contexts.SaveChangesAsync();

        TempData["SuccessMessage"] = "Event added successfully.";
        return RedirectToAction("Index");

    }

    [HttpGet]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Delete(int id, bool confirm = false)
    {
        var events = await _contexts.Events.FirstOrDefaultAsync(e => e.Id == id);
        if (events == null)
        {
            return NotFound();
        }
        ViewData["ConfirmDelete"] = confirm;
        return View(events);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var events = await _contexts.Events.FirstOrDefaultAsync(e => e.Id == id);
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

    [Authorize(Roles = "Student")]
    public async Task<IActionResult> Details(int id)
    {
        var eventDetails = await _contexts.Events
            .Include(e => e.UserEvents)
            .ThenInclude(ue => ue.User)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (eventDetails == null)
        {
            return NotFound();
        }

        ViewData["CurrentParticipants"] = eventDetails.UserEvents.Count;
        return View(eventDetails);
    }
    [Authorize(Roles = "Student")]
    [HttpPost]
    public async Task<IActionResult> JoinEvent(int id)
    {
        var user = await _userManager.GetUserAsync(User); // Récupère l'utilisateur actuel
        var eventToJoin = await _contexts.Events.Include(e => e.UserEvents).FirstOrDefaultAsync(e => e.Id == id);

        if (eventToJoin == null)
        {
            return NotFound();
        }

        // Vérifie si le nombre de participants est inférieur au nombre maximum
        if (eventToJoin.UserEvents.Count >= eventToJoin.MaxParticipants)
        {
            TempData["ErrorMessage"] = "Sorry, this event is full.";
            return RedirectToAction("Details", new { id });
        }

        // Vérifie si l'utilisateur est déjà inscrit
        var existingUserEvent = await _contexts.UserEvents
            .FirstOrDefaultAsync(ue => ue.UserId == user.Id && ue.EventId == eventToJoin.Id);

        if (existingUserEvent != null)
        {
            TempData["ErrorMessage"] = "You are already registered for this event.";
            return RedirectToAction("Details", new { id });
        }

        // Crée une nouvelle inscription
        var userEvent = new UserEvent
        {
            UserId = user.Id,
            EventId = eventToJoin.Id
        };

        _contexts.UserEvents.Add(userEvent);
        await _contexts.SaveChangesAsync();

        TempData["SuccessMessage"] = "Successfully registered for the event!";
        return RedirectToAction(nameof(Index));
    }

}

