using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace mvc.Models;

public enum Major
{
    CS, IT, MATHS, OTHER
}
public class Student : User
{
    // Variables d'instance
    public int Id { get; set; }
    [Required, StringLength(20)]
    public double GPA { get; set; }
    [Required]
    public Major Major { get; set; }
    public DateTime AdmissionDate { get; set; }
}