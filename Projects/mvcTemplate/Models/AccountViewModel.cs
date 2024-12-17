

namespace mvc.Models
{
    public class AccountViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string ConfirmedPassword { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int Age { get; set; }
        public DateTime AdmissionDate { get; set; }
        public Material Material { get; set; }
        public Major Major {get;set;}
    }
}
