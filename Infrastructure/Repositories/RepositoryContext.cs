using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new TaskConfiguration());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectUserTask>().HasKey(i => new { i.ProjectId, i.UserTaskId });
        }

        public DbSet<UserTask>? UserTasks { get; set; }
        public DbSet<Project>? Projects { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Notification>? Notifications { get; set; }
        public DbSet<ProjectUserTask>? ProjectUserTasks { get; set; }
    }
}
