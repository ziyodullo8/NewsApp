using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models
{
    public class NewsModels
    {
        public int Id { get; set; }
        [MinLength(4), MaxLength(100)]
        public string Title { get; set; }
        [MinLength(10)]
        public string Disciription { get; set; }
        public string ImageUrl { get; set; }
      
    }
}
