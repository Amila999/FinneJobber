using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinneJobber.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public DateTime Time { get; set; }
        public string Description { get; set; }
        [Required]
        public double Budget { get; set; }
    }
}
