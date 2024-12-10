using System.ComponentModel.DataAnnotations;

namespace mvc.Models;

public enum Major
{
    CS, IT, MATHS, OTHER
}
public class Student
{
    // Variables d'instance
    public int Id { get; set; }
    [Required, StringLength(20)]
    public string Firstname { get; set; }
    [Required]
    public string Lastname { get; set; }
    [Required]
    public int Age { get; set; }
    public double GPA { get; set; }
    [Required]
    public Major Major { get; set; }
    public DateTime AdmissionDate { get; set; }
}