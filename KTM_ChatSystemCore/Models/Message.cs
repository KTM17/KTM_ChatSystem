using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public int UserId { get; set; }

    public string? Content { get; set; }

    public DateTime? MessageTime { get; set; }

    public virtual User? User { get; set; } = null!;

    public Message()
    {
        MessageId = 0;
        UserId = 0;
        Content = string.Empty;
        MessageTime = DateTime.MinValue;
        User = new();
    }
}
