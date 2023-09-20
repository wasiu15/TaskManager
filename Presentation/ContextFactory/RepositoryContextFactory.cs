using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Presentation.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("sqlConnection");
            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly("Presentation"));
            return new RepositoryContext(builder.Options);




            //  THIS IS FOR SQLITE AND CO
            //var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            //var builder = new DbContextOptionsBuilder<RepositoryContext>()
            //    .UseSqlite(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Presentation"));
            //return new RepositoryContext(builder.Options);

        }
    }
}
