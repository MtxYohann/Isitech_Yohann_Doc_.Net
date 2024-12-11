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

    private string UserId => _userManager.GetUserId(User);

    public EventController(ApplicationDbContext context, UserManager<Teacher> userManager)
    {
        _contexts = context;
        _userManager = userManager;
    }

    public async Task<ActionResult> Index()
    {
        var events = await _contexts.Events.ToListAsync();
        return View(events);
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
}