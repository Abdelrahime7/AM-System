using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Hosting;
using System;


namespace Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
            optionsBuilder.UseNpgsql("Host = localhost; Port = 5432; " +
                "Database = AMS; Username = postgres; Password = 1234");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
}
