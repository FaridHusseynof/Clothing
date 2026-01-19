using System.ComponentModel.DataAnnotations;

namespace Clothing.Areas.AdminPanel.ViewModels
{
    public class CreateVM
    {
        [Required, Range(0, 10)]
        public int review { get; set; }
        [Required, Range(0, 1000)]
        public decimal price { get; set; }
        [Required, MaxLength(30)]
        public string title { get; set; }
        public string? imageURL { get; set; }
        [Required, MaxLength(60)]
        public string description { get; set; }
        public IFormFile? imageFile { get; set; }
    }
}
