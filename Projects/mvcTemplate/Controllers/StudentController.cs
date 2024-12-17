using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace mvc.Controllers;

public class StudentController : Controller
{
    private readonly ApplicationDbContext _contexts;
    private readonly UserManager<User> _usermanager;

    private List<User> students = new List<User>
    {
    };
    public StudentController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _contexts = context;
        _usermanager = userManager;
    }


    // GET: StudentController
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var students = await _usermanager.GetUsersInRoleAsync("Student");

        return View(students);
    }
    [HttpGet]
    public async Task<IActionResult> ShowDetails(Student model)
    {

        var student = await _usermanager.Users.FirstOrDefaultAsync(e => e.Id == model.Id);

        if (student == null)
        {
            return NotFound("Student not found");
        }

        return View(student);

    }

    // Modifier un Student
    [HttpGet]
    public async Task<IActionResult> Update(Student model)
    {
        var student = await _usermanager.Users.FirstOrDefaultAsync(e => e.Id == model.Id);
        if (student == null)
        {
            return NotFound("Student not found");
        }

        var viewModel = new StudentUpdateViewModel
        {
            Firstname = student.Firstname,
            Lastname = student.Lastname,
            Age = student.Age,
            Major = student.Major
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(StudentUpdateViewModel updatedStudent)
    {
        if (!ModelState.IsValid)
        {
            return View(updatedStudent);
        }

        var student = await _usermanager.Users.FirstOrDefaultAsync();
        if (student == null)
        {
            return NotFound("Student not found");
        }

        student.Firstname = updatedStudent.Firstname;
        student.Lastname = updatedStudent.Lastname;
        student.Age = updatedStudent.Age;
        student.Major = updatedStudent.Major;

        var result = await _usermanager.UpdateAsync(student);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Failed to update the student");
            return View(updatedStudent);
        }

        return RedirectToAction(nameof(Index));
    }



    // Supprimer un Student
    [HttpGet]
    public async Task<IActionResult> Delete(Student model)
    {
        var student = await _usermanager.Users.FirstOrDefaultAsync(e => e.Id == model.Id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(Student model)
    {
        var students = await _usermanager.Users.FirstOrDefaultAsync(e => e.Id == model.Id);
        if (students == null)
        {
            TempData["ErrorMessage"] = "Student not found.";
            return RedirectToAction(nameof(Index));
        }

        var result = await _usermanager.DeleteAsync(students);
        if (!result.Succeeded)
        {
            TempData["ErrorMessage"] = "Failed to delete the student.";
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = "Student deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}

