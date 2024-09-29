using Core.Models;
using Infrastructure.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces;

public interface IUserServices
{
    Task<UserResult?> CreateUser(User user);
    Task<UserResult?> GetUserByEmail(string email);
    Task<UserInitials?> GetUserInitials(int userId);
    Task<List<string>?> GetUserEmailIds();
    Task<bool?> UpdateUserEmail(int userId, string newEmail, string password);
    Task<bool?> UpdateUserPassword(int userId, string currentPassword, string newPassword);
    Task<bool?> UpdateUserName(int userId, string newUsername);
    Task<bool?> DeleteUser(int userId, string email, string password);
}
