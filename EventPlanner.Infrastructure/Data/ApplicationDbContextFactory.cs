using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EventPlanner.Infrastructure.Data;

// This factory explicitly tells EF Core tools (dotnet ef) how to create the DbContext
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // 1. Build configuration to find the connection string
        // We set the base path to look at the API project's directory for appsettings.json
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EventPlanner.Api"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        // 2. Get the connection string explicitly from the config
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("DefaultConnection connection string is missing or empty.");
        }

        // 3. Configure the DbContext options builder to use the correct string
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlite(connectionString);

        // 4. Return the configured DbContext
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}