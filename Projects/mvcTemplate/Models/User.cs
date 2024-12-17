using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace mvc.Models;
public class User : IdentityUser
{

    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateTime AdmissionDate { get; set; }
    public int Age { get; set; }
    public Material Material { get; set; }
    public Major Major { get; set; }

}