using FinneJobber.DataAccess.Repository.IRepository;
using FinneJobber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinneJobber.DataAccess.Repository.IRepository
{
    public interface IJobCartRepository : IRepository<JobCart>
    {
        bool makeApplied(JobCart jobCart, bool isApplied);
        bool cancelApplied(JobCart jobCart, bool isApplied);
    }
}
