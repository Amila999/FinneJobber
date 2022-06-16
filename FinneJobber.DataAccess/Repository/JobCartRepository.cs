using FinneJobber.DataAccess.Repository.IRepository;
using FinneJobber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinneJobber.DataAccess.Repository
{
    public class JobCartRepository : Repository<JobCart>, IJobCartRepository
    {
        private ApplicationDbContext _db;
        public JobCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public bool cancelApplied(JobCart jobCart, bool isApplied)
        {
            jobCart.IsApplied = false;
            return jobCart.IsApplied;
        }

        public bool makeApplied(JobCart jobCart, bool isApplied)
        {
            jobCart.IsApplied = true;
            return jobCart.IsApplied;
        }
    }
}
