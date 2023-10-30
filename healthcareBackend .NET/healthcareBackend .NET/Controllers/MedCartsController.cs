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
    public class MedCartsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedCartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MedCarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedCart>>> GetMedCart()
        {
			var medCarts = await _context.MedCart
	        .Include(mc => mc.MedItems) // Include the MedItems navigation property
	        .ToListAsync();

			if (_context.MedCart == null)
          {
              return NotFound();
          }
            return await _context.MedCart.ToListAsync();
        }

		// GET: api/MedCarts/5
		[HttpGet("{id}")]
		public async Task<ActionResult<MedCart>> GetMedCart(int id)
		{
			var medCart = await _context.MedCart
				.Include(mc => mc.MedItems) // Include the MedItems navigation property
				.FirstOrDefaultAsync(mc => mc.CartId == id);

			if (medCart == null)
			{
				return NotFound();
			}

			return medCart;
		}

		// PUT: api/MedCarts/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutMedCart(int id, MedCart medCart)
        {
            if (id != medCart.CartId)
            {
                return BadRequest();
            }

            _context.Entry(medCart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedCartExists(id))
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


        // POST: api/MedCarts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]

		public async Task<ActionResult<MedCart>> PostMedCart(MedCart medCart)
		{
			// Fetch the MedItem based on itemId
			var medItem = await _context.MedItems.FindAsync(medCart.ItemId);

			if (medItem == null)
			{
				return NotFound("MedItem not found");
			}

			// Fetch the MedCategory based on categoryId in the MedItem
			var medCategory = await _context.MedCategory.FindAsync(medItem.CategoryId);

			if (medCategory == null)
			{
				return NotFound("MedCategory not found");
			}

			// Set the MedItems and MedCategory properties
			medCart.MedItems = medItem;
			medCart.MedItems.MedCategory = medCategory;

			// Add the MedCart to the context
			_context.MedCart.Add(medCart);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetMedCart", new { id = medCart.CartId }, medCart);
		}

		// DELETE: api/MedCarts/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedCart(int id)
        {
            if (_context.MedCart == null)
            {
                return NotFound();
            }
            var medCart = await _context.MedCart.FindAsync(id);
            if (medCart == null)
            {
                return NotFound();
            }

            _context.MedCart.Remove(medCart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedCartExists(int id)
        {
            return (_context.MedCart?.Any(e => e.CartId == id)).GetValueOrDefault();
        }
    }
}
