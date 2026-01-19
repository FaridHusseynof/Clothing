using System.ComponentModel.DataAnnotations;

namespace Clothing.Models
{
    public class Product: BaseModel
    {
        [Required, Range(0, 10)]
        public int Review { get; set; }
        [Required, Range(0, 1000)]
        public decimal Price { get; set; }
        [Required, MaxLength(30)]
        public string Title { get; set; }
        public string ImageURL { get; set; }
        [Required, MaxLength(60)]
        public string Description { get; set; }
    }
}
