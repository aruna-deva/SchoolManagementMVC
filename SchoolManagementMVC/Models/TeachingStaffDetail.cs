using System.ComponentModel.DataAnnotations;
namespace SchoolManagementSystem.Models
{
	public class TeachingStaffDetail
	{
		[Required]
		public string TeacherName { get; set; }
		[Key]
		[Required]
		[Range(minimum:3,maximum:3,ErrorMessage ="Id length is exceeded")]
		public int TeacherId { get; set; }
		[Required(ErrorMessage="Teacher Qualification is required")]
		public string Qualification {get; set;}
		
        
		[Required]
		public int StaffTypeId {get; set;}

    }
}