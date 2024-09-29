using Azure.Core;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;

namespace Infrastructure.Repositories;
public interface IUserRepository
{
    bool UserExists(int id);
    bool UserExists(string email);
    Task<User?> CreateUserAsync(User user);
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByEmailIdAsync(string email);
    Task<List<string>?> GetUserEmailIdsAsync();
    Task<List<User>?> GetAllUsersAsync();
    Task<bool> UpdateUserAsync(User User);
    Task<bool> DeleteUserAsync(int userId);
}
public class UserRepository : IUserRepository
{
    private readonly KTM_CSContext _context;
    public UserRepository(KTM_CSContext context)
    {
        _context = context;
    }

    public UserRepository(UserRepository userRepository)
    {
        _context = userRepository._context;
    }

    public bool UserExists(int id)
    {
        return _context.Users.Any(e => e.UserId == id);
    }
    public bool UserExists(string email)
    {
        return _context.Users.Any(e => e.Email == email);
    }


    // Create Methods
    public async Task<User?> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Create OTP Record for User
        Otpauthentication OTP = new()
        {
            UserId = user.UserId,
            CreateTime = DateTime.UtcNow,
            Otp = 0
        };
        _context.Otpauthentications.Add(OTP);
        await _context.SaveChangesAsync();

        // Create Notification Records for User
        Notification notification = new()
        {
            Message = "Account Created Successfully",
            UserId = user.UserId,
            CreateTime = DateTime.UtcNow,
            Count = 1
        };
        _context.Notifications.Add(notification);

        notification = new()
        {
            Message = "Welcome to KTM Chat System",
            UserId = user.UserId,
            CreateTime = DateTime.UtcNow,
            Count = 2
        };
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        return user;
    }


    // Read Methods
    public async Task<User?> GetUserByIdAsync(int userId)
    {
        var user =  await _context.Users
                             .Where(u => u.UserId == userId)
                             .SingleOrDefaultAsync();
        return user;
    }

    public async Task<User?> GetUserByEmailIdAsync(string email)
    {
        return await _context.Users
                             .Where(u => u.Email.ToLower() == email.ToLower())
                             .SingleOrDefaultAsync();
    }

    public async Task<List<string>?> GetUserEmailIdsAsync()
    {
        var emails = await _context.Users
                                    .Select(user => user.Email)
                                    .ToListAsync();
        return emails;
    }

    public async Task<List<User>?> GetAllUsersAsync()
    {
        var users = await _context.Users.ToListAsync();

        return users;
    }


    // Update Methods
    public async Task<bool> UpdateUserAsync(User User)
    {
        var user = await _context.Users.FindAsync(User.UserId);
        if(user != null)
        {
            user.Email = User.Email;
            user.Name = User.Name;
            user.Password = user.Password;
            user.LastLogin = User.LastLogin;
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
        }
        return false;
    }


    // Delete Methods
    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user); 
            return await _context.SaveChangesAsync() > 0; 
        }
        return false;  
    }
}
