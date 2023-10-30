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
    public class MedItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MedItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedItems>>> GetMedItems()
        {
          if (_context.MedItems == null)
          {
              return NotFound();
          }
            return await _context.MedItems.ToListAsync();
        }

        // GET: api/MedItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedItems>> GetMedItems(int id)
        {
          if (_context.MedItems == null)
          {
              return NotFound();
          }
            var medItems = await _context.MedItems.FindAsync(id);

            if (medItems == null)
            {
                return NotFound();
            }

            return medItems;
        }

        // PUT: api/MedItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedItems(int id, MedItems medItems)
        {
            if (id != medItems.ItemId)
            {
                return BadRequest();
            }

            _context.Entry(medItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedItemsExists(id))
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

        // POST: api/MedItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MedItems>> PostMedItems(MedItems medItems)
		//{
		//  if (_context.MedItems == null)
		//  {
		//      return Problem("Entity set 'ApplicationDbContext.MedItems'  is null.");
		//  }
		//    _context.MedItems.Add(medItems);
		//    await _context.SaveChangesAsync();

		//    return CreatedAtAction("GetMedItems", new { id = medItems.ItemId }, medItems);
		//}
		{
			// Check if the category exists before creating the item
			var existingCategory = await _context.MedCategory.FindAsync(medItems.CategoryId);
			if (existingCategory == null)
			{
				// Return a bad request response because the category doesn't exist
				return BadRequest("The specified category doesn't exist.");
			}

			// Link the item to the existing category
			medItems.MedCategory = existingCategory;

			// Add the item to the context and save changes
			_context.MedItems.Add(medItems);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetMedItems", new { id = medItems.ItemId }, medItems);
		}



		// DELETE: api/MedItems/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedItems(int id)
        {
            if (_context.MedItems == null)
            {
                return NotFound();
            }
            var medItems = await _context.MedItems.FindAsync(id);
            if (medItems == null)
            {
                return NotFound();
            }

            _context.MedItems.Remove(medItems);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedItemsExists(int id)
        {
            return (_context.MedItems?.Any(e => e.ItemId == id)).GetValueOrDefault();
        }
    }
}
