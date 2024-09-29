using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models;

// Class Created to combine both Message and File as Chat
public class Chat
{
    public int ChatId { get; set; }

    public bool IsFile { get; set; }

    public DateTime UploadTime { get; set; }
}
