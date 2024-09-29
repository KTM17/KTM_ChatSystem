using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace Core.Models;

public class Password
{
    public static bool VerifyPassword(string inputPassword, string storedPasswordHash)
    {
        var inputPasswordHash = HashPassword(inputPassword);
        return inputPasswordHash == storedPasswordHash;
    }

    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
