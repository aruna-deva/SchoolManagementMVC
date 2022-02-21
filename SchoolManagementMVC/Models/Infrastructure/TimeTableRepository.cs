using SchoolManagementSystem.Models;
using SchoolManagementSystem.Infrastructure;
using System.Linq;
using System.Collections.Generic;

namespace SchoolManagementSystem.Infrastructure
{
    public class TimeTableRepository : ICRUDRepository<TimeTable,int>
    {
        SchoolManagementDbContext _db;
        public TimeTableRepository(SchoolManagementDbContext db)      
        {
            _db = db;
        }

        public void Create(TimeTable item)
        {
             _db.Timetable.Add(item);
            _db.SaveChanges();
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var obj=_db.Timetable.FirstOrDefault(c=>c.TimeTableId==id);
            if(obj==null)
                return;
            _db.Timetable.Remove(obj);
            _db.SaveChanges();
            //throw new NotImplementedException();
        }

        public IEnumerable<TimeTable> GetAll()
        {
            return _db.Timetable.ToList();
        }

        public TimeTable GetDetails(int id)
        {
            return _db.Timetable.FirstOrDefault(c => c.TimeTableId == id);
        }

        public void Update(TimeTable item)
        {
            var obj=_db.Timetable.FirstOrDefault(c=>c.TimeTableId==item.TimeTableId);
            if(obj==null)
                return;
                obj.ClassId=item.ClassId;
                obj.TeacherId=item.TeacherId;
                obj.SessionNumber=item.SessionNumber;
                obj.Timings=item.Timings;
                
                _db.Entry(obj).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}
