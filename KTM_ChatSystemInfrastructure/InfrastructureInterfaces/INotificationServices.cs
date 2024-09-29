using Core.Models;
using Infrastructure.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces;

public interface INotificationServices
{
    Task<UserResult?> AddNewNotification(Notification notification);

}
