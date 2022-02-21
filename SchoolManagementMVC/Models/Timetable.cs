
using System.ComponentModel.DataAnnotations;
namespace SchoolManagementSystem.Models
{

    public class TimeTable
    {
        [Required]
        public string ClassId { get; set; }
        [Key]
        [Required(ErrorMessage ="TimeTableId is required")]
        public int TimeTableId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int SessionNumber { get; set; }
        [Required]
        public string Timings { get; set; }
    }
}