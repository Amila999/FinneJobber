using FinneJobber.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinneJobber.DataAccess.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Job = new JobRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            JobCart = new JobCartRepository(_db);
        }
        public ICategoryRepository Category { get; private set; }
        public IJobRepository Job { get; private set;}
        public IJobCartRepository JobCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public void Save()
        {
             _db.SaveChanges();
        }
    } 
}
