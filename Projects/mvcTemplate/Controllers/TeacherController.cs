using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Data;

namespace mvc.Controllers;

public class TeacherController : Controller
{
    // champ prive pour stocker le dbcontext
    private readonly ApplicationDbContext contexts;

    // Constructeur
    public TeacherController(ApplicationDbContext context)
    {
        contexts = context;
    }

    // GET: TeacherController
    [HttpGet]
    public IActionResult Index()
    {
        return View(contexts.Teachers);
    }

    // Afficher le détails d'un Teacher
    [HttpGet]
    public IActionResult ShowDetails(int id)
    {
        var teacher = contexts.Teachers.FirstOrDefault(e => e.Id == id);
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

        // Mise à jour des propriétés de l'étudiant
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

