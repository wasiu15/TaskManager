using Domain.Entities;

namespace Application.Interfaces.Repositories;

    public interface INotificationRepository
    {
        void CreateNotification(Notification notification);
        void UpdateNotification(Notification notification);
        void DeleteNotification(Notification notification);
        Task<Notification> GetByNotificationId(string notificationId, bool trackChanges);
        Task<List<Notification>> GetNotifications();
        Task<Notification> GetByNotificationIdAndUserId(string taskId, string userId, bool trackChanges);
    }
