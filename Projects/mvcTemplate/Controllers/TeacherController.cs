using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace mvc.Controllers;

public class TeacherController : Controller
{
    // champ prive pour stocker le dbcontext
    private readonly ApplicationDbContext contexts;
    private readonly SignInManager<Teacher> _signInManager;
    private readonly UserManager<Teacher> _userManager;

    // Constructeur
    public TeacherController(ApplicationDbContext context, SignInManager<Teacher> signInManager, UserManager<Teacher> userManager)
    {
        contexts = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(Teacher model)
    {

        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager
            .PasswordSignInAsync(model.Firstname!, model.Password!, model.RememberMe, false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Erreur lors de la connexion");
            TempData["ErrorMessage"] = "Erreur lors de la connexion";
            return View(model);
        }

        TempData["SuccessMessage"] = "Bienvenue sur votre tableau de bord !";
        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(Teacher model)
    {

        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Erreur lors de l'inscription";
            return View(model);
        }

        var user = new Teacher()
        {
            Firstname = model.Firstname!,
            Lastname = model.Lastname!,
            Age = model.Age!,
            Material = model.Material!,
            AdmissionDate = model.AdmissionDate!
        };

        var result = await _userManager.CreateAsync(user, model.Password!);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            TempData["ErrorMessage"] = "Erreur lors de l'inscription";
            return View(model);
        }

        return RedirectToAction("Index", "Dashboard");

    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }

    // GET: TeacherController
    [HttpGet]
    public IActionResult Index()
    {
        return View(contexts.Teachers);
    }

    // Afficher le détails d'un Teacher
    [HttpGet]
    public async Task<IActionResult> ShowDetails(string id)
    {
        var teacher = await _userManager.FindByIdAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }
        return View(teacher);
    }

    // Ajouter un Teacher
    // Accesible via /Teacher/Add en GET affichera le formulaire
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    // Accesible via /Teacher/Add en POST
    [HttpPost]
    public IActionResult Add(Teacher teacher)
    {
        if (ModelState.IsValid)
        {
            contexts.Teachers.Add(teacher);
            contexts.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(contexts.Teachers);
    }

    // Modifier un Teacher
    [HttpGet]
    public IActionResult Update(int id)
    {
        var teacher = contexts.Teachers.FirstOrDefault(e => e.Id == id);
        if (teacher == null)
        {
            return NotFound();
        }
        return View(teacher);
    }
    [HttpPost]
    public IActionResult Update(Teacher updatedTeacher)
    {
        if (!ModelState.IsValid)
        {
            return View(updatedTeacher);
        }

        var teacher = contexts.Teachers.FirstOrDefault(e => e.Id == updatedTeacher.Id);
        if (teacher == null)
        {
            return NotFound();
        }

        // Mise à jour des propriétés de l'ensseignant
        teacher.Firstname = updatedTeacher.Firstname;
        teacher.Lastname = updatedTeacher.Lastname;
        teacher.Age = updatedTeacher.Age;
        teacher.AdmissionDate = updatedTeacher.AdmissionDate;
        teacher.Material = updatedTeacher.Material;

        contexts.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    // Supprimer un Teacher
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var teacher = contexts.Teachers.FirstOrDefault(e => e.Id == id);
        if (teacher == null)
        {
            return NotFound();
        }
        return View(teacher);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var teacher = contexts.Teachers.FirstOrDefault(e => e.Id == id);
        if (teacher != null)
        {
            contexts.Teachers.Remove(teacher);
            contexts.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}

