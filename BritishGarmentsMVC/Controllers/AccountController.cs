using Azure.Core;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Identity.UI.Services;
using BritishGarmentsMVC.Models;
using Microsoft.EntityFrameworkCore;

public class AccountController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpPost("api/forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword model)
    {
        var emailExist = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
        if (emailExist != null)
        {
            return StatusCode(200, new { message = "Email found" });
        }
        else
        {
            return StatusCode(401, new { message = "Email not found" });
        }
        
    }

    [HttpPost("api/reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
    {
        try
        {
            var emailExist = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            //Alter the password
            if(emailExist == null)
                return BadRequest("Error resetting password");

            emailExist.PasswordHash = model.NewPassword;
            _context.SaveChanges();
            return StatusCode(200, new { message = "Password has been reset" });

        }
        catch
        {
            return BadRequest("Error resetting password");
        }
    }

}
