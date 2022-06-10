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
                objFromDb.JobLocation = obj.JobLocation;
                objFromDb.Budget = obj.Budget;
                objFromDb.Date = obj.Date;
                objFromDb.Time = obj.Time;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Category = obj.Category;
            }
        }
    }
}
