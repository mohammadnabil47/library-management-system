using LibraryManagement.Application.DTOs;

namespace LibraryManagement.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<IEnumerable<BookDto>> GetAllBooksWithCategoriesAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<BookDto> CreateBookAsync(CreateBookDto createBookDto);
        Task<BookDto> UpdateBookAsync(UpdateBookDto updateBookDto);
        Task<bool> DeleteBookAsync(int id);
        Task<IEnumerable<BookDto>> GetBooksByCategoryIdAsync(int categoryId);
    }
}
