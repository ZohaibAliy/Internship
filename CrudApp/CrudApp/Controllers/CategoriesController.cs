using CrudApp.Data;
using CrudApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        [Route("GetCategory")]
        public async Task<ActionResult<IEnumerable<Catagories>>> GetCatagories()
        {
            return await _context.Catagories.ToListAsync();
        }

        [HttpGet("GetCategorybyid")]
        
        public async Task<ActionResult<Catagories>> GetCatagoriesbyid(int id)
        {
            var catagories = await _context.Catagories.FindAsync(id);
          
            if (catagories == null)
            {
                return NotFound();
            }

            return catagories;
        }
        [HttpPost("AddCatagory")]
        public async Task<ActionResult<Catagories>> AddCatagories(Catagories catagories)
        {
            _context.Catagories.Add(catagories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCtagories", new { id = catagories.Id }, catagories);
        }
        [HttpPut("UpdataCategory")]
        public async Task<IActionResult> UpdateCatagories(int id, Catagories catagories)
        {
            if (id != catagories.Id)
            {
                return BadRequest();
            }

            _context.Entry(catagories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatagoriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCatagories(int id)
        {
            var catagories = await _context.Catagories.FindAsync(id);
            if (catagories == null)
            {
                return NotFound();
            }

            _context.Catagories.Remove(catagories);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatagoriesExists(int id)
        {
            return _context.Catagories.Any(e => e.Id == id);
        }

    }
}
