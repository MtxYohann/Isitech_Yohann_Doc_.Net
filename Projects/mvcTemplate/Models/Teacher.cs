namespace mvc.Models;

public enum Material
{
    CS, IT, MATHS, OTHER
}
public class Teacher
{
    // Variables d'instance
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public int Age { get; set; }
    public Material Material { get; set; }
    public DateTime AdmissionDate { get; set; }
}