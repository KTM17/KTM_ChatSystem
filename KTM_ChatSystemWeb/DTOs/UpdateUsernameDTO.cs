using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DTOs;

public class UpdateUsernameDTO
{
    [Required(ErrorMessage = "New Username is required.")]
    [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
    public string NewUserName { get; set; } = null!;
    public UpdateUsernameDTO(string userName)
    {
        NewUserName = userName;
    }
    public bool CompareUsername(string userName)
    {
        return userName == NewUserName;
    }

}
