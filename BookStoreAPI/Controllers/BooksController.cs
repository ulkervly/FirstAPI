using BookStoreAPI.DAL;
using BookStoreAPI.DTOs;
using BookStoreAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<Book> books = _context.Books.ToList();

            IEnumerable<BookGetDto> bookDtos = new List<BookGetDto>();

            bookDtos = books.Select(x => new BookGetDto { Id = x.Id, Name = x.Name, Price = x.Price });

            return Ok(bookDtos);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _context.Books.Find(id);

            if (book is null) return NotFound();

            BookGetDto dto = new BookGetDto()
            {
                Id = book.Id,
                Name = book.Name,
                Price = book.Price,
            };

            return Ok(dto);

        }
        [HttpPost]
        public IActionResult Create(BookCreateDto dto)
        {
            Book book = new Book()
            {
                Name = dto.Name,
                Price = dto.Price,
                CostPrice = dto.CostPrice,
            };

            book.CreatedDate = DateTime.UtcNow.AddHours(4);
            book.UpdatedDate = DateTime.UtcNow.AddHours(4);
            book.CatagoryId = dto.CatagoryId;

            book.IsDeleted = false;
            _context.Books.Add(book);
            _context.SaveChanges();

            return StatusCode(201, new { message = "Object created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BookUpdateDto dto)
        {
            var book = _context.Books.Find(id);

            if (book is null)
            {
                return NotFound();
            }

            book.Name = dto.Name;
            book.Price = dto.Price;
            book.CostPrice = dto.CostPrice;
            book.UpdatedDate = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return StatusCode(201,new { message = "Object updated" });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);

            if (book is null)
            {
                return NotFound();
            }

            book.IsDeleted = true;
            book.UpdatedDate = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return StatusCode(201,new { message = "Object deleted" });
        }


    }
}
