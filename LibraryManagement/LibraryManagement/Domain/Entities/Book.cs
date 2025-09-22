using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string Author { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string? ISBN { get; set; }
        
        public DateTime PublishedDate { get; set; }
        
        public int Quantity { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
        
        // Navigation properties
        public virtual ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }
}
