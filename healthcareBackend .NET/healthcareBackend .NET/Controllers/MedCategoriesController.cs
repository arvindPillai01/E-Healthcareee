using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using healthcareBackend_.NET.Data;
using healthcareBackend_.NET.Models;

namespace healthcareBackend_.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MedCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedCategory>>> GetMedCategory()
        {
          if (_context.MedCategory == null)
          {
              return NotFound();
          }
            return await _context.MedCategory.ToListAsync();
        }

        // GET: api/MedCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedCategory>> GetMedCategory(int id)
        {
          if (_context.MedCategory == null)
          {
              return NotFound();
          }
            var medCategory = await _context.MedCategory.FindAsync(id);

            if (medCategory == null)
            {
                return NotFound();
            }

            return medCategory;
        }

        // PUT: api/MedCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedCategory(int id, MedCategory medCategory)
        {
            if (id != medCategory.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(medCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedCategoryExists(id))
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

        // POST: api/MedCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MedCategory>> PostMedCategory(MedCategory medCategory)
        {
          if (_context.MedCategory == null)
          {
              return Problem("Entity set 'ApplicationDbContext.MedCategory'  is null.");
          }
            _context.MedCategory.Add(medCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedCategory", new { id = medCategory.CategoryId }, medCategory);
        }

        // DELETE: api/MedCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedCategory(int id)
        {
            if (_context.MedCategory == null)
            {
                return NotFound();
            }
            var medCategory = await _context.MedCategory.FindAsync(id);
            if (medCategory == null)
            {
                return NotFound();
            }

            _context.MedCategory.Remove(medCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedCategoryExists(int id)
        {
            return (_context.MedCategory?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
