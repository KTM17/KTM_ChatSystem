using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime LastLogin { get; set; }

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Otpauthentication Otpauthentication { get; set; } = new Otpauthentication();

    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        LastLogin = DateTime.UtcNow;
    }

    public User()
    {

    }

}
