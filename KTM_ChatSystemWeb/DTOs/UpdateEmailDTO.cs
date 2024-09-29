using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DTOs;

public class UpdateEmailDTO
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [StringLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
    public string NewEmail { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
    public string Password { get; set; } = null!;

    public UpdateEmailDTO(string email, string password)
    {
        NewEmail = email;
        Password = password;
    }
    public bool CompareEmail(string emailId)
    {
        return emailId == NewEmail;
    }
}
