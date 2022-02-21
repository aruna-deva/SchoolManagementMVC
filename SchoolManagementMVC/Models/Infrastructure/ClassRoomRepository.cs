using System.Collections.Generic;
using System.Linq;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Infrastructure
{
    public class ClassRoomRepository : ICRUDRepository<ClassRoom, int> 
    {
        SchoolManagementDbContext _db;
        public ClassRoomRepository(SchoolManagementDbContext db)
        {
            _db = db;
        }

        public void Create(ClassRoom item)
        {
             _db.Classroom.Add(item);
            _db.SaveChanges();
            //throw new NotImplementedException();
        }

        //public void Delete(string standard)
        public void Delete(int classroomId)
        {
            var obj=_db.Classroom.FirstOrDefault(c=>c.ClassroomId==classroomId);
            if(obj==null)
                return;
            _db.Classroom.Remove(obj);
            _db.SaveChanges();
            //throw new NotImplementedException();
        }

        public IEnumerable<ClassRoom> GetAll()
        {
            return _db.Classroom.ToList();
        }
        public ClassRoom GetDetails(int classroomId)
        {
            return _db.Classroom.FirstOrDefault(c=>c.ClassroomId==classroomId);
        }

        public void Update(ClassRoom item)
        {
            var obj=_db.Classroom.FirstOrDefault(c=>c.ClassroomId==item.ClassroomId);
            if(obj==null)
                return;
            obj.Standard = item.Standard;
                obj.Section=item.Section;
                _db.Entry(obj).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}