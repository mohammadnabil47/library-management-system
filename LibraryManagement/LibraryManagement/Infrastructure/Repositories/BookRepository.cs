using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> GetBooksWithCategoriesAsync()
        {
            return await _dbSet
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .ToListAsync();
        }

        public async Task<Book?> GetBookWithCategoriesByIdAsync(int id)
        {
            return await _dbSet
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryIdAsync(int categoryId)
        {
            return await _dbSet
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .Where(b => b.BookCategories.Any(bc => bc.CategoryId == categoryId))
                .ToListAsync();
        }
    }
}
