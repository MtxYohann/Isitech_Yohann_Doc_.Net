using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Controllers;

public class StudentController : Controller
{
    //Création d'une liste statique de Student
    private static List<Student> students = new()
    {
        new() { AdmissionDate = new DateTime(2021, 5, 5), Age = 19, Firstname = "Yohann", GPA = 5.5, Id = 1, Lastname = "Mathieux", Major = Major.IT },
        new() { AdmissionDate = new DateTime(2021, 9, 1), Age = 20, Firstname = "Patoche", GPA = 3.5, Id = 2, Lastname = "Ladébrouille", Major = Major.OTHER },
    };

    // GET: StudentController
    public ActionResult Index()
    {
        return View(students);
    }
    public IActionResult ShowDetails(int id)
    {
        var student = students.FirstOrDefault(e => e.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Add(Student student)
    {
        if (ModelState.IsValid)
        {
            student.Id = students.Max(e => e.Id) + 1;
            students.Add(student);
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }


    public IActionResult Update(int id)
    {
        var student = students.FirstOrDefault(e => e.Id == id);
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

        var student = students.FirstOrDefault(e => e.Id == updatedStudent.Id);
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

        return RedirectToAction(nameof(Index));
    }
    public IActionResult Delete(int id)
    {
        var student = students.FirstOrDefault(e => e.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var student = students.FirstOrDefault(e => e.Id == id);
        if (student != null)
        {
            students.Remove(student);
        }
        return RedirectToAction(nameof(Index));
    }
}

