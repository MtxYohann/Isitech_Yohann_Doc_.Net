using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Data;
namespace mvc.Controllers;

public class StudentController : Controller
{
    // champ prive pour stocker le dbcontext
    private readonly ApplicationDbContext contexts;

    // Constructeur
    public StudentController(ApplicationDbContext context)
    {
        contexts = context;
    }

    // GET: StudentController
    [HttpGet]
    public IActionResult Index()
    {
        return View(contexts.Students);
    }

    // Afficher le détails d'un Student
    [HttpGet]
    public IActionResult ShowDetails(int id)
    {
        var student = contexts.Students.FirstOrDefault(e => e.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // Ajouter un Student
    // Accesible via /Student/Add en GET affichera le formulaire
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    // Accesible via /Student/Add en POST
    [HttpPost]
    public IActionResult Add(Student student)
    {

        if (ModelState.IsValid)
        {
            contexts.Students.Add(student);
            contexts.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(contexts.Students);
    }

    // Modifier un Student
    [HttpGet]
    public IActionResult Update(int id)
    {
        var student = contexts.Students.FirstOrDefault(e => e.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }
    [HttpPost]
    public IActionResult Update(Student updatedStudent)
    {
        if (!ModelState.IsValid)
        {
            return View(updatedStudent);
        }

        var student = contexts.Students.FirstOrDefault(e => e.Id == updatedStudent.Id);
        if (student == null)
        {
            return NotFound();
        }

        // Mise à jour des propriétés de l'étudiant
        student.Firstname = updatedStudent.Firstname;
        student.Lastname = updatedStudent.Lastname;
        student.Age = updatedStudent.Age;
        student.AdmissionDate = updatedStudent.AdmissionDate;
        student.GPA = updatedStudent.GPA;
        student.Major = updatedStudent.Major;

        contexts.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    // Supprimer un Student
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var student = contexts.Students.FirstOrDefault(e => e.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var student = contexts.Students.FirstOrDefault(e => e.Id == id);
        if (student != null)
        {
            contexts.Students.Remove(student);
            contexts.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}

