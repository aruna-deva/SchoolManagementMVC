using System.ComponentModel.DataAnnotations;
namespace SchoolManagementSystem.Models
{
    public class Staffclassification
    {
        [Key]
        [Required(ErrorMessage ="StaffTypeId is required")]
        public int StaffTypeId {get; set;}
        [Required(ErrorMessage ="TypeName is required")]
        [StringLength(maximumLength:50,ErrorMessage ="Maxinumlength is exceeded")]
        public string TypeName {get; set;}
    }
}