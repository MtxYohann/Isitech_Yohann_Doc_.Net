namespace mvc.Models
{

    public class StudentUpdateViewModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public Major Major { get; set; }
    }
}