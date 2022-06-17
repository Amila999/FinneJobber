using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinneJobber.Models
{
    public class JobCart
    {
        public int Id { get; set; }

        public int JobId { get; set; }
        [ForeignKey("JobId")]
        [ValidateNever]
        public Job Job { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public bool IsApplied { get; set; }
    }
}
