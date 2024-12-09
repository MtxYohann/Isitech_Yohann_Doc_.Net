using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Controllers;

public class TeacherController : Controller
{
    //Création d'une liste statique de Teacher
    private static List<Teacher> teachers = new()
    {
        new() { AdmissionDate = new DateTime(1950, 8, 15), Age = 118, Firstname = "Josette", Id = 1, Lastname = "Raginel", Material = Material.OTHER },
        new() { AdmissionDate = new DateTime(1988, 4, 3), Age = 55, Firstname = "Patrick", Id = 1, Lastname = "Fambon", Material = Material.OTHER },
    };

    // GET: TeacherController
    public ActionResult Index()
    {
        return View(teachers);
    }

}

