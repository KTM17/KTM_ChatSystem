using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Models;

public class UserResult
{
    public int UserId { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime LastLogin { get; set; }


    public UserResult(User? user = null)
    {
        if (user != null)
        {
            UserId = user.UserId;
            Name = user.Name;
            Email = user.Email;
            LastLogin = user.LastLogin;
        }
    }
}
