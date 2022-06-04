using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinneJobber.Models.ViewModels
{
    public class JobVM
    {
        public Job Job { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
