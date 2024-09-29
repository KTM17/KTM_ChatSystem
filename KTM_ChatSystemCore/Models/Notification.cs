using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public string? Message { get; set; }

    public DateTime? CreateTime { get; set; }

    public int? Count { get; set; }

    public virtual User User { get; set; } = null!;

    public Notification(int userId = 0)
    {
        UserId = userId;
        Message = null;
        CreateTime = null;
        Count = 0;
    }
}
