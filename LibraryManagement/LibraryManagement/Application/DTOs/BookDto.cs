namespace LibraryManagement.Application.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<CategorySimpleDto> Categories { get; set; } = new();
    }

    public class CategorySimpleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int Quantity { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
    }

    public class UpdateBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int Quantity { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
