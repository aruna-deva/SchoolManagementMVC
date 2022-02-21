using SchoolManagementSystem.Models;
using SchoolManagementSystem.Infrastructure;
using System.Linq;
using System.Collections.Generic;

namespace SchoolManagementSystem.Infrastructure
{
    public class StaffClassificationRepository : ICRUDRepository<Staffclassification, int>
    {
        SchoolManagementDbContext _db;
        public StaffClassificationRepository(SchoolManagementDbContext db)
        {
            _db = db;
        }

        public void Create(Staffclassification item)
        {
            _db.Staffclassifications.Add(item);
            _db.SaveChanges();
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var obj = _db.Staffclassifications.FirstOrDefault(c => c.StaffTypeId == id);
            if (obj == null)
                return;
            _db.Staffclassifications.Remove(obj);
            _db.SaveChanges();
            //throw new NotImplementedException();
        }

        public IEnumerable<Staffclassification> GetAll()
        {
            return _db.Staffclassifications.ToList();
            //throw new NotImplementedException();
        }

        public Staffclassification GetDetails(int id)
        {
            return _db.Staffclassifications.FirstOrDefault(c => c.StaffTypeId == id);
            //throw new NotImplementedException();
        }

        public void Update(Staffclassification item)
        {
            var obj = _db.Staffclassifications.FirstOrDefault(c => c.StaffTypeId == item.StaffTypeId);
            if (obj == null)
                return;
            obj.TypeName = item.TypeName;

            _db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}
