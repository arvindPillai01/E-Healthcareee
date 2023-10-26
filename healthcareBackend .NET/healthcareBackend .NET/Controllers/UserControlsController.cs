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
    public class UserControlsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserControlsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserControls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserControl>>> GetUserControl()
        {
          if (_context.UserControl == null)
          {
              return NotFound();
          }
            return await _context.UserControl.ToListAsync();
        }

        // GET: api/UserControls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserControl>> GetUserControl(int id)
        {
          if (_context.UserControl == null)
          {
              return NotFound();
          }
            var userControl = await _context.UserControl.FindAsync(id);

            if (userControl == null)
            {
                return NotFound();
            }

            return userControl;
        }


		// PUT: api/UserControls/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutUserControl(int id, UserControl userControl)
        {
            if (id != userControl.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userControl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserControlExists(id))
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

        // POST: api/UserControls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserControl>> PostUserControl(UserControl userControl)
        {
            if (_context.UserControl == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserControl'  is null.");
            }


            var usernameExists = await CheckUsernameExists(userControl.UserName);

            if (usernameExists)
            {
	            return Conflict("Username already exists. Please choose a different username.");
            }


			_context.UserControl.Add(userControl);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserControl", new { id = userControl.UserId }, userControl);


        }

        // DELETE: api/UserControls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserControl(int id)
        {
            if (_context.UserControl == null)
            {
                return NotFound();
            }
            var userControl = await _context.UserControl.FindAsync(id);
            if (userControl == null)
            {
                return NotFound();
            }

            _context.UserControl.Remove(userControl);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        // Authentication endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
	        var user = await _context.UserControl
		        .SingleOrDefaultAsync(u => u.UserName == loginRequest.Username);

	        if (user == null)
	        {
		        return NotFound("User not found");
	        }

	        if (user.Password == loginRequest.Password)
	        {
		        // Password matches; user is authenticated
		        // You can return a token or other authentication response here
		        return Ok("Authentication successful");
	        }
	        else
	        {
		        // Password does not match
		        return Unauthorized("Authentication failed");
	        }
        }

        // GETapi/UserControls/registration
        [HttpGet("check-username/{username}")]
        private async Task<bool> CheckUsernameExists(string username)
        {
	        var existingUser = await _context.UserControl.FirstOrDefaultAsync(u => u.UserName == username);
	        return existingUser != null;
        }




		private bool UserControlExists(int id)
        {
            return (_context.UserControl?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
