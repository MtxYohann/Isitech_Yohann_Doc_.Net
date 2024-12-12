using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace mvc.Controllers;

public class TeacherController : Controller
{
    private readonly ApplicationDbContext _contexts;
    private readonly UserManager<Teacher> _usermanager;

    private List<Teacher> teachers = new List<Teacher>
    {
    };
    public TeacherController(ApplicationDbContext context, UserManager<Teacher> userManager)
    {
        _contexts = context;
        _usermanager = userManager;
    }


    // GET: TeacherController
    [HttpGet]
    public async Task<IActionResult> Index(int id)
    {
        var teachers = await _usermanager.Users.ToListAsync<Teacher>();

        return View(teachers);
    }
    [HttpGet]
    public async Task<IActionResult> ShowDetails()
    {

        var teacher = await _usermanager.Users.FirstOrDefaultAsync();

        if (teacher == null)
        {
            return NotFound("Teacher not found");
        }

        return View(teacher);

    }

    // Modifier un Teacher
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var teacher = await _usermanager.Users.FirstOrDefaultAsync();
        if (teacher == null)
        {
            return NotFound("Teacher not found");
        }

        var viewModel = new TeacherUpdateViewModel
        {
            Firstname = teacher.Firstname,
            Lastname = teacher.Lastname,
            Age = teacher.Age,
            Material = teacher.Material
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(TeacherUpdateViewModel updatedTeacher)
    {
        if (!ModelState.IsValid)
        {
            return View(updatedTeacher);
        }

        var teacher = await _usermanager.Users.FirstOrDefaultAsync();
        if (teacher == null)
        {
            return NotFound("Teacher not found");
        }

        teacher.Firstname = updatedTeacher.Firstname;
        teacher.Lastname = updatedTeacher.Lastname;
        teacher.Age = updatedTeacher.Age;
        teacher.Material = updatedTeacher.Material;

        var result = await _usermanager.UpdateAsync(teacher);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Failed to update the teacher");
            return View(updatedTeacher);
        }

        return RedirectToAction(nameof(Index));
    }



    // Supprimer un Teacher
    [HttpGet]
    public async Task<IActionResult> Delete()
    {
        var teacher = await _usermanager.Users.FirstOrDefaultAsync();
        if (teacher == null)
        {
            return NotFound();
        }
        return View(teacher);
    }
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(Teacher model)
    {
        var teachers = await _usermanager.Users.FirstOrDefaultAsync(e => e.Id == model.Id);
        if (teachers == null)
        {
            TempData["ErrorMessage"] = "Teacher not found.";
            return RedirectToAction(nameof(Index));
        }

        var result = await _usermanager.DeleteAsync(teachers);
        if (!result.Succeeded)
        {
            TempData["ErrorMessage"] = "Failed to delete the teacher.";
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = "Teacher deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}

