using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace mvc.Models;

public enum Material
{
    CS, IT, MATHS, OTHER
}
public class Teacher : User
{
    // Variables d'instance
    public Material Material { get; set; }
    public DateTime AdmissionDate { get; set; }
}