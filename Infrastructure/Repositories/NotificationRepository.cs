using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateNotification(Notification notification) => Create(notification);
        public void UpdateNotification(Notification notification) => Update(notification);
        public void DeleteNotification(Notification notification) => Delete(notification);
        public async Task<List<Notification>> GetNotifications() => await FindAll(false).ToListAsync();
        public async Task<Notification> GetByNotificationId(string notificationId, bool trackChanges) => await FindByCondition(x => x.NotificationId.Equals(notificationId), trackChanges).FirstOrDefaultAsync();
        public async Task<Notification> GetByNotificationIdAndUserId(string taskId, string userId, bool trackChanges) => await FindByCondition(x => x.TaskId.Equals(taskId) && x.RecievedUserId.Equals(userId), trackChanges).FirstOrDefaultAsync();
    }
}
