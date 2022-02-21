using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class StudentDetail
    {
        [Key]
        [Required(ErrorMessage ="StudentId is required")]
        public int StudentID {get; set;}
        [Required(ErrorMessage ="StudentName is required")]
        public string StudentName {get; set;}
        [Required(ErrorMessage ="FatherName is required")]
        public string FatherName {get; set;}
        [Required(ErrorMessage ="ClassId is required")]
        public string ClassId {get; set;}
        public DateTime BirthDate {get; set;}
        public string BloodGroup {get; set;}
        [Required]
        public string Address {get; set;}
        public string City {get; set;}
        public string Nationality {get; set;}
        [Required]
        [StringLength(maximumLength:10,ErrorMessage ="Maximum length is 10")]
        public string ContactNumber {get; set;}
    }
}