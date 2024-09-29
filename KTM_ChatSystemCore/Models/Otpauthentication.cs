using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Otpauthentication
{
    public int UserId { get; set; }

    public int? Otp { get; set; }

    public DateTime? CreateTime { get; set; }

    public virtual User User { get; set; } = null!;
}
