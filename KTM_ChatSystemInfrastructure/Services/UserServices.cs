using Core.Models;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class UserServices : IUserServices
{
    protected readonly IUserRepository _userRepository;

    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResult?> CreateUser(User user)
    {
        if (_userRepository.UserExists(user.Email))
        {
            return null;
        }
        var createdUser = await _userRepository.CreateUserAsync(user);

        return new UserResult(createdUser);
    }

    public async Task<UserResult?> GetUserByEmail(string email)
    {
        if (!_userRepository.UserExists(email))
        {
            return null;
        }
        return new UserResult( await _userRepository.GetUserByEmailIdAsync(email));
    }

    public async Task<List<string>?> GetUserEmailIds()
    { 
         return await _userRepository.GetUserEmailIdsAsync();
    }

    public async Task<UserInitials?> GetUserInitials(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user == null)
        {
            return new UserInitials
            {
                FirstNameInitial = ' ',
                LastNameInitial = ' '
            };

        }
        char userFirstName = user.Name[0];
        char userLastName = ' ';
        if (user.Name.Contains(' '))
        {
            var spaceIndex = user.Name.IndexOf(' ');
            if (spaceIndex >= 0 && spaceIndex < user.Name.Length - 1)
            {
                userLastName = user.Name[spaceIndex + 1];
            }
        }
        UserInitials userInitials = new()
        {
            FirstNameInitial = userFirstName,
            LastNameInitial = userLastName
        };

        return userInitials;
    }

    public async Task<bool?> UpdateUserEmail(int userId, string newEmail, string password)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if(user == null)
        {
            return null;
        }
        if (user.Password.Equals(Password.HashPassword(password)))
        {
            return false;
        }
        if (user.Email.Equals(newEmail, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        user.Email = newEmail;
        return await _userRepository.UpdateUserAsync(user);

    }
    public async Task<bool?> UpdateUserPassword(int userId, string currentPassword, string newPassword)
    {
        if(currentPassword == newPassword)
        {
            return false;
        }
        var user = await _userRepository.GetUserByIdAsync(userId);
        if(user == null || user.Password.Equals(currentPassword) || !user.Password.Equals(newPassword))
        {
            return null;
        }
        if (user.Password.Equals(Password.HashPassword(currentPassword)))
        {
            return false;
        }
        user.Password = newPassword;
        return await _userRepository.UpdateUserAsync(user);
    }
    public async Task<bool?> UpdateUserName(int userId, string name)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if(user == null)
        {
            return null;
        }
        if(user.Name == name)
        {
            return false;
        }
        user.Name = name;
        return await _userRepository.UpdateUserAsync(user);
    }

    public async Task<bool?> DeleteUser(int userId, string email, string password)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if(user == null)
        {
            return null;
        }
        if(!user.Email.Equals(email, StringComparison.OrdinalIgnoreCase) || !user.Password.Equals(Password.HashPassword(password))){
            return false;
        }
        return await _userRepository.DeleteUserAsync(userId);
    }
}
