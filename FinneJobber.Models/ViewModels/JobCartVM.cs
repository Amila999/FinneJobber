using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinneJobber.Models.ViewModels
{
    public class JobCartVM
    {
        public IEnumerable<JobCart> ListJobCart { get; set; }
        public double JobCartTotal { get; set; }
    }
}
