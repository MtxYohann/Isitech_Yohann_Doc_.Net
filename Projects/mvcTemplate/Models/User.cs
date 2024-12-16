using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace mvc.Models;
public class User : IdentityUser
{

    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateTime AdmissionDate { get; set; }
    public int Age { get; set; }
    public string ConfirmedPassword { get; set; }


}