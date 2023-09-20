using Application.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskmanagerV2.Domain.Enums;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public NotificationsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpGet("getAllNotifications")]
        public async Task<ActionResult> GetAll()
        {
            var response = await _serviceManager.NotificationService.GetAllNotifications();
            if (response.IsSuccessful)
                return Ok(response);
            return NotFound();
        }

        [HttpGet("getNotificationById")]
        public async Task<ActionResult> GetNotificationById([FromQuery] string notificationId)
        {
            if (string.IsNullOrEmpty(notificationId))
                return BadRequest(notificationId);
            else
            {
                var response = await _serviceManager.NotificationService.GetByNotificationId(notificationId);
                return Ok(response);
            }
        }

        [HttpPost("addNotification")]
        public async Task<ActionResult> AddNotification(CreateNotificationRequest notification)
        {
            if (string.IsNullOrEmpty(notification.TaskId) || string.IsNullOrEmpty(notification.Message))
                return BadRequest(notification);
            //  CHECK IF THE NOTIFICATION TYPE ENTERED IS ONE OF OUR CUSTOM NOTIFICATION TYPE
            else if (notification.Type != NotificationType.Status_update && notification.Type != NotificationType.Due_date)
                return BadRequest(notification);
            else
            {
                var response = await _serviceManager.NotificationService.CreateNotification(notification);
                return Ok(response);
            }
        }

        [HttpPatch("ReadOrUnread")]
        public async Task<ActionResult> UpdateNotification([FromQuery] string notificationId)
        {
            if (string.IsNullOrEmpty(notificationId))
                return BadRequest(notificationId);
            else
            {
                var response = await _serviceManager.NotificationService.ReadOrUnread(notificationId);
                return Ok(response);
            }
        }

        [HttpDelete("DeleteNotification")]
        public async Task<ActionResult> DeleteNotification([FromQuery] string notificationId)
        {
            if (string.IsNullOrEmpty(notificationId))
                return BadRequest(notificationId);
            else
            {
                var response = await _serviceManager.NotificationService.DeleteNotification(notificationId);
                return Ok(response);
            }
        }

    }
}
