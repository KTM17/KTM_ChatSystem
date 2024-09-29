using Core.Models;
using Infrastructure.Interfaces;
using Infrastructure.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTM_ChatSystemInfrastructure.Services
{
    public class NotificationServices : INotificationServices
    {
        public Task<UserResult?> AddNewNotification(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
