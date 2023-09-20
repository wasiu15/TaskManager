using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskmanagerV2.Domain.Enums;

namespace Infrastructure.Repositories.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<UserTask>
{
    public void Configure(EntityTypeBuilder<UserTask> builder)
    {
        builder.HasData
        (
            new UserTask
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Fitness Goals",
                Description = "Gym workout",
                DueDate = new DateTime(2023, 11, 10),
                Priority = Priority.Medium,
                UserId = Guid.NewGuid().ToString(),
                Status = Status.Pending,
            },
            new UserTask
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Work Project Task",
                Description = "Research industry trends",
                DueDate = new DateTime(2023, 10, 12),
                Priority = Priority.Medium,
                UserId = Guid.NewGuid().ToString(),
                Status = Status.In_progress,
            }
        );
    }
}
