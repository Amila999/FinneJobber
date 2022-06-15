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
    }
}
