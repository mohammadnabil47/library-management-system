using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksWithCategoriesAsync()
        {
            var books = await _bookRepository.GetBooksWithCategoriesAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookWithCategoriesByIdAsync(id);
            return book != null ? _mapper.Map<BookDto>(book) : null;
        }

        public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
        {
            var book = _mapper.Map<Book>(createBookDto);
            
            // Add book categories
            foreach (var categoryId in createBookDto.CategoryIds)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category != null)
                {
                    book.BookCategories.Add(new BookCategory
                    {
                        BookId = book.Id,
                        CategoryId = categoryId,
                        Book = book,
                        Category = category
                    });
                }
            }

            var createdBook = await _bookRepository.AddAsync(book);
            return _mapper.Map<BookDto>(createdBook);
        }

        public async Task<BookDto> UpdateBookAsync(UpdateBookDto updateBookDto)
        {
            var existingBook = await _bookRepository.GetBookWithCategoriesByIdAsync(updateBookDto.Id);
            if (existingBook == null)
                throw new ArgumentException("Book not found");

            _mapper.Map(updateBookDto, existingBook);
            
            // Update book categories
            existingBook.BookCategories.Clear();
            foreach (var categoryId in updateBookDto.CategoryIds)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category != null)
                {
                    existingBook.BookCategories.Add(new BookCategory
                    {
                        BookId = existingBook.Id,
                        CategoryId = categoryId,
                        Book = existingBook,
                        Category = category
                    });
                }
            }

            await _bookRepository.UpdateAsync(existingBook);
            return _mapper.Map<BookDto>(existingBook);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return false;

            await _bookRepository.DeleteAsync(book);
            return true;
        }

        public async Task<IEnumerable<BookDto>> GetBooksByCategoryIdAsync(int categoryId)
        {
            var books = await _bookRepository.GetBooksByCategoryIdAsync(categoryId);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }
    }
}
