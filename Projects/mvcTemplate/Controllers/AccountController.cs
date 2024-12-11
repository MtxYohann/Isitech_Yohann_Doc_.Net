using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Data;

public class AccountController : Controller
{
    private readonly ApplicationDbContext contexts;
    private readonly SignInManager<Teacher> _signInManager;
    private readonly UserManager<Teacher> _userManager;
    public AccountController(ApplicationDbContext context, SignInManager<Teacher> signInManager, UserManager<Teacher> userManager)
    {
        contexts = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(AccountViewModel model)
    {

        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Erreur lors de l'inscription";
            return View(model);
        }

        var user = new Teacher()
        {
            UserName = model.Firstname + model.Lastname,
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Email = model.Email,
            Age = model.Age,
            Material = model.Material,
            AdmissionDate = model.AdmissionDate,
            ConfirmedPassword = model.ConfirmedPassword
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(model);

    }


    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}
