 using SchoolManagementSystem.Models;
 using SchoolManagementSystem.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagementSystem.Infrastructure
{
    public class StudentDetailRepository : ICRUDRepository<StudentDetail, int>
    {
        SchoolManagementDbContext _db;
        public StudentDetailRepository(SchoolManagementDbContext db)
        {
            _db = db;
        }
        public IEnumerable<StudentDetail> GetAll()
        {
            return _db.StudentDetails.ToList();
        }
        public StudentDetail GetDetails(int id)
        {
            return _db.StudentDetails.FirstOrDefault(c=>c.StudentID==id);
        }
        public void Create(StudentDetail item)
        {
            _db.StudentDetails.Add(item);
            _db.SaveChanges();
            //throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            var obj=_db.StudentDetails.FirstOrDefault(c=>c.StudentID==id);
            if(obj==null)
                return;
            _db.StudentDetails.Remove(obj);
            _db.SaveChanges();
            //throw new NotImplementedException();
        }

        public void Update(StudentDetail item)
        {
            var obj=_db.StudentDetails.FirstOrDefault(c=>c.StudentID==item.StudentID);
            if(obj==null)
                return;
                obj.StudentName=item.StudentName;
                obj.FatherName=item.FatherName;
                obj.ClassId=item.ClassId;
                obj.BirthDate=item.BirthDate;
                obj.BloodGroup=item.BloodGroup;
                obj.Address=item.Address;
                obj.City=item.City;
                obj.Nationality=item.Nationality;
                obj.ContactNumber=item.ContactNumber;
                _db.Entry(obj).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}