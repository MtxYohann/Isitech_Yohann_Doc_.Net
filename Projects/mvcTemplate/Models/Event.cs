using System.ComponentModel.DataAnnotations;

namespace mvc.Models;

public class Event
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Title { get; set; }
    [Required]
    [StringLength(500)]
    public string Description { get; set; }
    [Required]
    [Display(Name = "Date of event")]
    [DataType(DataType.DateTime)]
    public DateTime EventDate { get; set; }
    [Required]
    [Range(10, 200)]
    [Display(Name = "Maximum number of participants")]
    public int MaxParticipants { get; set; }
    [Required]
    [StringLength(100)]
    public string Location { get; set; }
    [Display(Name = "Creation date")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
