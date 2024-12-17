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
    public string Firstname {get;set; }
    public string Lastname {get;set; }
    public int Age {get;set; }
    [Required, StringLength(20)]
    
    public Major Major { get; set; }
    public DateTime AdmissionDate { get; set; }
}