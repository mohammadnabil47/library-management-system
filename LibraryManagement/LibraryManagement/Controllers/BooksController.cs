using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IAdoBookService _adoBookService;

        public BooksController(IBookService bookService, IAdoBookService adoBookService)
        {
            _bookService = bookService;
            _adoBookService = adoBookService;
        }

        /// <summary>
        /// Get all books
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        /// <summary>
        /// Get all books with categories using EF Core
        /// </summary>
        [HttpGet("with-categories")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooksWithCategories()
        {
            var books = await _bookService.GetAllBooksWithCategoriesAsync();
            return Ok(books);
        }

        /// <summary>
        /// Get all books with categories using ADO.NET stored procedure
        /// </summary>
        [HttpGet("with-categories-ado")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooksWithCategoriesAdo()
        {
            var books = await _adoBookService.GetAllBooksWithCategoriesAsync();
            return Ok(books);
        }

        /// <summary>
        /// Get book by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        /// <summary>
        /// Get books by category ID
        /// </summary>
        [HttpGet("by-category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByCategoryId(int categoryId)
        {
            var books = await _bookService.GetBooksByCategoryIdAsync(categoryId);
            return Ok(books);
        }

        /// <summary>
        /// Create a new book
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(CreateBookDto createBookDto)
        {
            try
            {
                var book = await _bookService.CreateBookAsync(createBookDto);
                return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update an existing book
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> UpdateBook(int id, UpdateBookDto updateBookDto)
        {
            if (id != updateBookDto.Id)
                return BadRequest("ID mismatch");

            try
            {
                var book = await _bookService.UpdateBookAsync(updateBookDto);
                return Ok(book);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a book
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
