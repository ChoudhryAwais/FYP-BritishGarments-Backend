using BritishGarmentsMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BritishGarmentsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController(ApplicationDbContext context) : ControllerBase
    {

        private readonly ApplicationDbContext _context = context;


        // Register a new user
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] User model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
                //check for existing user
                if (existingUser != null)
                    return StatusCode(409,new { message = "Email is already registered" });
                // Create a new User entity
                var user = new User
                {
                    Username = model.Username,
                    PasswordHash = model.PasswordHash, // Make sure to hash the password properly
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserAddress = model.UserAddress,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true,
                    Role = model.Role
                };

                // Add the new user to the database
                _context.Users.Add(user);

                try
                {
                    // Save changes to the database
                    await _context.SaveChangesAsync();
                    return StatusCode(200,new { message = "User registered successfully" });
                }
                catch (DbUpdateException ex)
                {
                    // Log the exception (not shown here)
                    return StatusCode(500, new { message = "An error occurred while registering the user", error = ex.Message });
                }
            }

            return BadRequest(ModelState);
        }

    }
}
