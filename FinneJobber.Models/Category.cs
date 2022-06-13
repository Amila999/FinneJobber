using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinneJobber.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
