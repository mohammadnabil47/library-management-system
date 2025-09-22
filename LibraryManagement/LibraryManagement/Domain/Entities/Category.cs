using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
        
        // Navigation properties
        public virtual ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }
}
