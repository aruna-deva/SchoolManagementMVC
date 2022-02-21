using System.ComponentModel.DataAnnotations;
namespace SchoolManagementSystem.Models
{

    public class ClassRoom
    {
        [Key]
        [Required(ErrorMessage ="ClassroomId is required")]
        public int ClassroomId {get; set;}
        [Required]
        public string Standard { get; set; }
        [Required]
        public string  Section { get; set; }
    }
}
