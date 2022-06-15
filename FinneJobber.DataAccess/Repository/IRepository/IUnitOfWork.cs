using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinneJobber.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IJobRepository Job { get; }
        IJobCartRepository JobCart { get; }
        IApplicationUserRepository ApplicationUser { get; }
        void Save();
    }
}