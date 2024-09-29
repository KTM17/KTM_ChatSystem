using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DTOs;

public class UpdatePasswordDTO
{
    [Required(ErrorMessage = "Current Password is required.")]
    [StringLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
    public string CurrentPassword { get; set; } = null!;

    [Required(ErrorMessage = "New Password is required.")]
    [StringLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
    public string NewPassword { get; set; } = null!;
}
