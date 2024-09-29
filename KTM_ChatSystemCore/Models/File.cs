using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class File
{
    public int FileId { get; set; }

    public int UserId { get; set; }

    public string? FileName { get; set; }

    public string? FileType { get; set; }

    public double FileSize { get; set; }

    public string? FilePath { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? UploadDate { get; set; }

    public string? Message { get; set; }

    public virtual User User { get; set; } = null!;
}
