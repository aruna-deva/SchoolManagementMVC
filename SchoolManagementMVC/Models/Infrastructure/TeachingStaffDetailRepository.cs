using SchoolManagementSystem.Models;
using SchoolManagementSystem.Infrastructure;
using System.Linq;
using System.Collections.Generic;

namespace SchoolManagementSystem.Infrastructure
{
    public class TeachingStaffDetailRepository : ICRUDRepository<TeachingStaffDetail, int>
    {
        SchoolManagementDbContext _db;
        public TeachingStaffDetailRepository(SchoolManagementDbContext db)      
        {
            _db = db;
        }

        public void Create(TeachingStaffDetail item)
        {
             _db.TeachingStaffDetails.Add(item);
            _db.SaveChanges();
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var obj=_db.TeachingStaffDetails.FirstOrDefault(c=>c.TeacherId==id);
            if(obj==null)
                return;
            _db.TeachingStaffDetails.Remove(obj);
            _db.SaveChanges();
            //throw new NotImplementedException();
        }

        public IEnumerable<TeachingStaffDetail> GetAll()
        {
             return _db.TeachingStaffDetails.ToList();
            //throw new NotImplementedException();
        }

        public TeachingStaffDetail GetDetails(int id)
        {
            return _db.TeachingStaffDetails.FirstOrDefault(c=>c.TeacherId==id);
            //throw new NotImplementedException();
        }

        public void Update(TeachingStaffDetail item)
        {
            var obj=_db.TeachingStaffDetails.FirstOrDefault(c=>c.TeacherId==item.TeacherId);
            if(obj==null)
                return;
                obj.TeacherName=item.TeacherName;
                obj.Qualification=item.Qualification;
                obj.StaffTypeId=item.StaffTypeId;
                
                _db.Entry(obj).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}
