using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Web.DTOs;
using Core.Models;
using Infrastructure.Services;
using Infrastructure.Services.Models;


using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Web.Controllers;

public class UserController(IUserServices userServices) : ControllerBase
{
    private readonly IUserServices _userService = userServices;

    // Create Method
    [HttpPost("CreateUser")]
    public async Task<ActionResult<UserResult>> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new User()
        {
            Name = createUserDto.Name,
            Email = createUserDto.Email,
            Password = Password.HashPassword(createUserDto.Password),
            LastLogin = DateTime.UtcNow
        };

        var createdUser = await _userService.CreateUser(user);

        return Ok(createdUser);
    }

    // Read Methods 
    [HttpGet("GetUserEmailIds")]
    public async Task<ActionResult<List<string>>> GetUserEmailIds()
    {
        try
        {
            var emails = await _userService.GetUserEmailIds();
            if(emails!= null)
            {
                return Ok(emails);
            }
            return NotFound("No Users Found.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Some exception occurred while obtaining email IDs");
        }

    }

    [HttpGet("GetUserInitials")]
    public Task<ActionResult<UserInitials>> GetUserInitials(int userId)
    {
        var userInitials = _userService.GetUserInitials(userId);

        
        return Task.FromResult<ActionResult<UserInitials>>(Ok(userInitials));
    }

    [HttpGet("GetUserByEmail")]
    public async Task<ActionResult<UserResult>> GetUserByEmail(string email)
    {
        var userResult = await _userService.GetUserByEmail(email);
        if (userResult == null)
        {
            return NotFound("User does not exist.");
        }
        return userResult;
    }


    // Update Methods
    [HttpPut("{userId}/UpdateEmail")]
    public async Task<IActionResult> UpdateUserEmail(int userId, [FromBody] UpdateEmailDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userResult = await _userService.UpdateUserEmail(userId, request.NewEmail, request.Password);
        
        if (userResult == null)
        {
            return NotFound("User not found");
        }
        if(userResult == false)
        {
            return BadRequest("Incorrect Password");
        }
        return Ok("Email updated successfully");
        

    }

    [HttpPut("{userId}/UpdatePassword")]
    public async Task<IActionResult> UpdateUserPassword(int userId, [FromBody] UpdatePasswordDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userResult = await _userService.UpdateUserPassword(userId, request.CurrentPassword, request.NewPassword);

        if (userResult == null)
        {
            return NotFound("User not found.");
        }

        if (userResult == false)
        {
            return BadRequest("Current / New password is incorrect");
        }

        return Ok("Password updated successfully.");
       
    }
    [HttpPut("{userId}/UpdateUsername")]
    public async Task<IActionResult> UpdateUsername(int userId, [FromBody] UpdateUsernameDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userService.UpdateUserName(userId, request.NewUserName);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (user == false)
        {
            return BadRequest("New Username cannot be same as old Username");
        }

        return Ok("Username updated successfully.");
    }


    // Delete Method
    [HttpDelete("{userId}/UpdateUsername")]
    public async Task<IActionResult> DeleteUser(int userId, [FromBody] DeleteUserDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userService.DeleteUser(userId, request.Email, request.Password);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (user == false)
        {
            return BadRequest("Invalid Email / Password");
        }

        return Ok("User Deleted Successfully.");
    }



}
