using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace mvc.Models;

public enum Material
{
    CS, IT, MATHS, OTHER
}
public class Teacher : IdentityUser
{
    // Variables d'instance
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public bool RememberMe { get; set; }
    public int Age { get; set; }
    public Material Material { get; set; }
    public DateTime AdmissionDate { get; set; }
    public string ConfirmedPassword { get; set; }
}