using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    
    public class User
    {
        public int Id{get; set;}
        [Required]
        public int StaffTypeId{get; set;}
        [Required]
        public string TypeName{get; set;}
        
        public void Deconstruct(out int staffTypeId,out string typeName)
        {
            staffTypeId=this.StaffTypeId;
            typeName=this.TypeName;
        }
    }
    public class Role
        {   
           [Key] public int RoleId {get; set;}
            public string RoleName {get; set;}
        }
    public enum RoleName
        {
            Principal,
            VicePrincipal,
            Administrator
        }

    }