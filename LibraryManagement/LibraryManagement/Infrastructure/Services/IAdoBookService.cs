using LibraryManagement.Application.DTOs;

namespace LibraryManagement.Infrastructure.Services
{
    public interface IAdoBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksWithCategoriesAsync();
    }
}
