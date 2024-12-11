using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Data;
using Microsoft.AspNetCore.Authorization;


namespace mvc.Controllers;

public class TeacherController : Controller
{
    private readonly ApplicationDbContext _contexts;

    private List<Teacher> teachers = new List<Teacher>
    {
    };
    public TeacherController(ApplicationDbContext context)
    {
        _contexts = context;
    }

    // GET: TeacherController
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        //var teachers = await _contexts.Teachers.ToListAsync();

        return View(teachers);
    }
    [HttpGet]
    public IActionResult ShowDetails(int id)
    {
        //var teacher = _context.Teachers.Find(id);
        // return View(teacher);
        return View();
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
        // Declencher le mecanisme de validation
        if (!ModelState.IsValid)
        {
            return View();
        }
        // Ajouter le teacher
        // _context.Teachers.Add(teacher);

        // Sauvegarder les changements
        _contexts.SaveChanges();
        return RedirectToAction("Index");
    }
    // Modifier un Teacher
    //[HttpGet]
    // public IActionResult Update(int id)
    // {
    //     var teacher = _contexts.Teachers.FirstOrDefault(e => e.Id == id);
    //     if (teacher == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(teacher);
    // }
    // [HttpPost]
    // public IActionResult Update(Teacher updatedTeacher)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return View(updatedTeacher);
    //     }
    //     var teacher = _contexts.Teachers.FirstOrDefault(e => e.Id == updatedTeacher.Id);
    //     if (teacher == null)
    //     {
    //         return NotFound();
    //     }
    //     // Mise à jour des propriétés de l'étudiant
    //     teacher.Firstname = updatedTeacher.Firstname;
    //     teacher.Lastname = updatedTeacher.Lastname;
    //     teacher.Age = updatedTeacher.Age;
    //     teacher.AdmissionDate = updatedTeacher.AdmissionDate;
    //     teacher.Material = updatedTeacher.Material;
    //     _contexts.SaveChanges();
    //     return RedirectToAction(nameof(Index));
    // }
    // // Supprimer un Teacher
    // [HttpGet]
    // public IActionResult Delete(int id)
    // {
    //     var teacher = _contexts.Teachers.FirstOrDefault(e => e.Id == id);
    //     if (teacher == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(teacher);
    // }
    // [HttpPost, ActionName("Delete")]
    // public IActionResult DeleteConfirmed(int id)
    // {
    //     var teacher = _contexts.Teachers.FirstOrDefault(e => e.Id == id);
    //     if (teacher != null)
    //     {
    //         _contexts.Teachers.Remove(teacher);
    //         _contexts.SaveChanges();
    //     }
    //     return RedirectToAction(nameof(Index));
    // }
}

