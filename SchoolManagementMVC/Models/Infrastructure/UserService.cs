using SchoolManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SchoolManagementSystem.Infrastructure
{
    public interface IUserService
    {
        bool Authenticate(User item);
        Role GetUserRole(int id);
        List<User> GetAll();
        User GetDetails(int id);
    }
    public class UserService : IUserService
    {
        SchoolManagementDbContext _context;
        public UserService (SchoolManagementDbContext context)
        {
            _context=context;
        }
        public bool Authenticate(User item)
        {
            var obj=_context.Users.FirstOrDefault(
                c=>c.StaffTypeId.Equals(item.StaffTypeId) &&
                    c.TypeName.Equals(item.TypeName)
            );
            if(obj !=null)
            {
                item.Id=obj.Id;
            
                return true;
            }
            else 
                return false;
        }
        public Role GetUserRole(int id)
        {
          var roles = _context.Roles.FromSqlRaw(
                $"SELECT RoleId, RoleName FROM Roles WHERE RoleId IN " + 
                $" (SELECT RoleID FROM UserRoles WHERE UserId={id})"
            );
            if(roles.Count()==0)
            return null; 
            else 
            return roles.First();  
        }
        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }
        public User GetDetails(int id)
        {
            throw new NotImplementedException();
        }
    }
}