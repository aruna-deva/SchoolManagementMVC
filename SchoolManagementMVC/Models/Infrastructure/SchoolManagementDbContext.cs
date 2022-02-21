using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Infrastructure
{
    public class SchoolManagementDbContext : DbContext
    {
        public SchoolManagementDbContext(DbContextOptions<SchoolManagementDbContext> options) : base(options) { }
        

        public DbSet<StudentDetail> StudentDetails {get; set;}
        public DbSet<TimeTable> Timetable {get; set;}
        public DbSet<ClassRoom> Classroom {get; set;}
        public DbSet<TeachingStaffDetail> TeachingStaffDetails {get; set;}
        public DbSet<Staffclassification> Staffclassifications {get; set;}
        public DbSet<User> Users {get; set;}
        public DbSet<Role> Roles {get; set;}
    }

}