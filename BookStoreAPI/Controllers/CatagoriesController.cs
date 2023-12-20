using BookStoreAPI.DAL;
using BookStoreAPI.DTOs;
using BookStoreAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace catagoryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CatagoriesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<Catagory> catagories = _context.Catagories.ToList();

            IEnumerable<CatagoryGetDto> ctgDtos = new List<CatagoryGetDto>();

            ctgDtos = catagories.Select(x => new CatagoryGetDto { Id = x.Id, Name = x.Name });

            return Ok(ctgDtos);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ctg = _context.Catagories.Find(id);

            if (ctg is null) return NotFound();

            CatagoryGetDto dto = new CatagoryGetDto()
            {
                Id = ctg.Id,
                Name = ctg.Name,
               
            };

            return Ok(dto);

        }
        [HttpPost]
        public IActionResult Create(CatagoryCreateDto dto)
        {
            Catagory catagory = new Catagory()
            {
                Name = dto.Name,
                
            };

            catagory.CreatedDate = DateTime.UtcNow.AddHours(4);
            catagory.UpdatedDate = DateTime.UtcNow.AddHours(4);
            catagory.IsDeleted = false;
            _context.Catagories.Add(catagory);
            _context.SaveChanges();

            return StatusCode(201, new { message = "Object created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CatagoryUpdateDto dto)
        {
            var catagory = _context.Catagories.Find(id);

            if (catagory is null)
            {
                return NotFound();
            }

            catagory.Name = dto.Name;
            catagory.UpdatedDate = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return StatusCode(201, new { message = "Object updated" });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var catagory = _context.Catagories.Find(id);

            if (catagory is null)
            {
                return NotFound();
            }

            catagory.IsDeleted = true;
            catagory.UpdatedDate = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return StatusCode(201, new { message = "Object deleted" });
        }
    }
}
