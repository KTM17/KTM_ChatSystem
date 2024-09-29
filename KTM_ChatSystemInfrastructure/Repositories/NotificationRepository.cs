using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public interface INotificationRepository
{
    Task<Notification?> CreateNofiticationAsync(Notification notification);
    Task<Notification?> GetNotificationByIdAsync(int notificationId);
    Task<int> GetNotificationCount(int userId);
    Task<List<Notification>?> GetAllNotificationsByUserIdAsync(int userId);
    Task<bool> DeleteNotificationAsync(int notificationId);
    Task<bool> DeleteNotificationAsync(int userId, int notificationCount);

}
public class NotificationRepository : INotificationRepository
{
    private readonly KTM_CSContext _context;

    public NotificationRepository(KTM_CSContext context)
    {
        _context = context;
    }
    public NotificationRepository(NotificationRepository notificationRepository)
    {
        _context = notificationRepository._context;
    }


    // Create Methods
    public async Task<Notification?> CreateNofiticationAsync(Notification notification)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var maxCount = await _context.Notifications
                                         .Where(n => n.UserId == notification.UserId)
                                         .MaxAsync(n => (int?)n.Count) ?? 0;

            if (maxCount == 10)
            {
                await _context.Notifications
                              .Where(n => n.UserId == notification.UserId && n.Count < 10)
                              .ForEachAsync(n => n.Count += 1);

                var notificationToDelete = await _context.Notifications
                                                         .Where(n => n.UserId == notification.UserId && n.Count == 1)
                                                         .FirstOrDefaultAsync();
                if (notificationToDelete != null)
                {
                    _context.Notifications.Remove(notificationToDelete);
                }
            }



            notification.Count = Math.Min(maxCount + 1, 10);

            await _context.Notifications.AddAsync(notification);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return notification;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return null;
        }
    }


    // Read Methods
    public async Task<List<Notification>?> GetAllNotificationsByUserIdAsync(int userId)
    {
        var notifications = await _context.Notifications.Where(notifications => notifications.UserId == userId).ToListAsync();

        return notifications;
    }

    public async Task<Notification?> GetNotificationByIdAsync(int notificationId)
    {
        var notification = await _context.Notifications
                             .Where(u => u.NotificationId == notificationId)
                             .SingleOrDefaultAsync();
        return notification;
    }

    public async Task<int> GetNotificationCount(int userId)
    {
        var maxCount = await _context.Notifications
                                 .Where(notification => notification.UserId == userId)
                                 .MaxAsync(notification => (int?)notification.Count);
        return maxCount?? 0;
    }


    // Delete Methods
    public async Task<bool> DeleteNotificationAsync(int notificationId)
    {
        var notification = await _context.Notifications.FindAsync(notificationId);
        if (notification != null)
        {
            _context.Notifications.Remove(notification);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<bool> DeleteNotificationAsync(int userId, int notificationCount)
    {
        var notification = await _context.Notifications
                                         .Where(n => n.UserId == userId && n.Count == notificationCount)
                                         .FirstOrDefaultAsync();

        if (notification != null)
        {
            _context.Notifications.Remove(notification);
            return await _context.SaveChangesAsync() > 0; 
        }
        return false; 
    }

}
