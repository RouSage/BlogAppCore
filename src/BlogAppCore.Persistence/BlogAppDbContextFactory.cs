using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BlogAppCore.Persistence
{
    public class BlogAppDbContextFactory : IDesignTimeDbContextFactory<BlogAppCoreDbContext>
    {
        private const string ConnectionStringName = "BlogAppCore";
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public BlogAppCoreDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}BlogAppCore", Path.DirectorySeparatorChar);

            return Create(basePath, Environment.GetEnvironmentVariable(AspNetCoreEnvironment));
        }

        private BlogAppCoreDbContext Create(string basePath, string environmentName)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional : false, reloadOnChange : true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional : true)
                .AddEnvironmentVariables()
                .Build();

            // Get connection string
            var connectionString = config.GetConnectionString(ConnectionStringName);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{ConnectionStringName}' is NULL or EMPTY", nameof(connectionString));
            }

            var optionsBuilder = new DbContextOptionsBuilder<BlogAppCoreDbContext>();
            optionsBuilder.UseSqlite(connectionString, opt => opt.MigrationsAssembly(typeof(BlogAppCoreDbContext).Assembly.FullName));

            return new BlogAppCoreDbContext(optionsBuilder.Options);
        }
    }
}