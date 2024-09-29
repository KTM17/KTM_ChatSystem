using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Web.Controllers;

public class NotificationController(INotificationServices notificationService) : ControllerBase
{
    private readonly INotificationServices _notificationService = notificationService;
}
