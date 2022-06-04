using FinneJobber.DataAccess.Repository.IRepository;
using FinneJobber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinneJobber.DataAccess.Repository
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        private ApplicationDbContext _db;
        public JobRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public void Update(Job obj)
        {
            var objFromDb = _db.Jobs.FirstOrDefault(u=>u.Id==obj.Id);
            if (objFromDb != null) 
            {
                obj.JobLocation = objFromDb.JobLocation;
                obj.Budget = objFromDb.Budget;
                obj.Date = objFromDb.Date;
                obj.Time = objFromDb.Time;
                obj.Description = objFromDb.Description;
                obj.CategoryId = obj.CategoryId;
                obj.Category = obj.Category;

            }
        }
    }
}
