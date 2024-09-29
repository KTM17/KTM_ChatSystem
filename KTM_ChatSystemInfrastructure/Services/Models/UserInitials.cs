using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Models;
public class UserInitials
{
    public char FirstNameInitial { get; set; }
    public char? LastNameInitial { get; set; }

    public UserInitials(char firstNameInitial = ' ', char lastNameInitial = ' ')
    {
        FirstNameInitial = firstNameInitial;
        LastNameInitial = lastNameInitial;
    }
}
