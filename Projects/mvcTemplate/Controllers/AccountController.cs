using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Data;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _contexts;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AccountController(ApplicationDbContext context, SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _contexts = context;
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult TeacherRegister()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> TeacherRegister(AccountViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var teacher = new User
        {
            UserName = model.Email,
            Email = model.Email,
            Firstname = model.Firstname,
            Lastname = model.Lastname
        };

        var result = await _userManager.CreateAsync(teacher, model.Password);
        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync("Prof"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Prof"));
            }
            await _userManager.AddToRoleAsync(teacher, "Prof");

            await _signInManager.SignInAsync(teacher, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }
    [HttpGet]
    public IActionResult StudentRegister()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> StudentRegister(AccountViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Erreur lors de l'inscription";
            return View(model);
        }

        var student = new User()
        {
            UserName = model.Firstname + model.Lastname,
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Email = model.Email,
            Age = model.Age,
            AdmissionDate = model.AdmissionDate,
        };

        var result = await _userManager.CreateAsync(student, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(student, "Student");

            TempData["SuccessMessage"] = "Successful registration !";
            return RedirectToAction("Index", "Home");
        }

        TempData["ErrorMessage"] = "Registration error";
        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager
            .PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe, false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Erreur lors de la connexion");
            TempData["ErrorMessage"] = "Erreur lors de la connexion";
            return View(model);
        }

        TempData["SuccessMessage"] = "Bienvenue sur votre tableau de bord !";
        return RedirectToAction("Index", "Home");

    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}
