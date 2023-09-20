namespace TaskmanagerV2.Domain.Enums;

public enum Priority
{
    None = 0,
    Low = 1,
    Medium = 2,
    High = 3
}
public enum Status
{
    None = 0,
    Pending = 1,
    In_progress = 2,
    completed = 3
}
public enum NotificationType
{
    Due_date = 1,
    Status_update = 2
}
public enum NotificationStatus
{
    Read = 2,
    Unread = 1
}
public enum AddOrDelete
{
    Add = 1,
    Delete = 0
}
