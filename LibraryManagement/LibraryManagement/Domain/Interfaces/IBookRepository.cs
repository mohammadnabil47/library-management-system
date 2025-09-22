using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksWithCategoriesAsync();
        Task<Book?> GetBookWithCategoriesByIdAsync(int id);
        Task<IEnumerable<Book>> GetBooksByCategoryIdAsync(int categoryId);
    }
}
