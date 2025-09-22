using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryManagement.Infrastructure.Services
{
    public class AdoBookService : IAdoBookService
    {
        private readonly string _connectionString;

        public AdoBookService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksWithCategoriesAsync()
        {
            var books = new List<BookDto>();
            var bookCategories = new Dictionary<int, List<CategoryDto>>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("GetAllBooksWithCategories", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var bookId = reader.GetInt32("BookId");
                var bookTitle = reader.GetString("BookTitle");
                var bookAuthor = reader.GetString("BookAuthor");
                var bookISBN = reader.IsDBNull("BookISBN") ? null : reader.GetString("BookISBN");
                var bookPublishedDate = reader.GetDateTime("BookPublishedDate");
                var bookQuantity = reader.GetInt32("BookQuantity");
                var bookCreatedDate = reader.GetDateTime("BookCreatedDate");
                var bookUpdatedDate = reader.IsDBNull("BookUpdatedDate") ? (DateTime?)null : reader.GetDateTime("BookUpdatedDate");

                if (!books.Any(b => b.Id == bookId))
                {
                    books.Add(new BookDto
                    {
                        Id = bookId,
                        Title = bookTitle,
                        Author = bookAuthor,
                        ISBN = bookISBN,
                        PublishedDate = bookPublishedDate,
                        Quantity = bookQuantity,
                        CreatedDate = bookCreatedDate,
                        UpdatedDate = bookUpdatedDate,
                        Categories = new List<CategorySimpleDto>()
                    });
                }

                if (!reader.IsDBNull("CategoryId"))
                {
                    var categoryId = reader.GetInt32("CategoryId");
                    var categoryName = reader.GetString("CategoryName");
                    var categoryDescription = reader.IsDBNull("CategoryDescription") ? null : reader.GetString("CategoryDescription");
                    var categoryCreatedDate = reader.GetDateTime("CategoryCreatedDate");
                    var categoryUpdatedDate = reader.IsDBNull("CategoryUpdatedDate") ? (DateTime?)null : reader.GetDateTime("CategoryUpdatedDate");

                    var book = books.First(b => b.Id == bookId);
                    if (!book.Categories.Any(c => c.Id == categoryId))
                    {
                        book.Categories.Add(new CategorySimpleDto
                        {
                            Id = categoryId,
                            Name = categoryName,
                            Description = categoryDescription,
                            CreatedDate = categoryCreatedDate,
                            UpdatedDate = categoryUpdatedDate
                        });
                    }
                }
            }

            return books;
        }
    }
}
