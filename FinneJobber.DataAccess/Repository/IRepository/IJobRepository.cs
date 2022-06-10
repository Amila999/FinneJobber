using FinneJobber.DataAccess.Repository.IRepository;
using FinneJobber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinneJobber.DataAccess.Repository.IRepository
{
    public interface IJobRepository : IRepository<Job>
    {
        void Update(Job obj);
    }
}
