using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BlogAppCore.Persistence
{
    public class BlogAppDbContextFactory : IDesignTimeDbContextFactory<BlogAppCoreDbContext>
    {
        private const string UserSecretsId = "9b3fd8b3-34c8-4d7c-b2de-edf0381518fb";
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
                .AddUserSecrets(UserSecretsId)
                .AddEnvironmentVariables()
                .Build();

            // Get connection string
            var connectionString = config.GetConnectionString("BlogAppCore");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{ConnectionStringName}' is NULL or EMPTY", nameof(connectionString));
            }

            var optionsBuilder = new DbContextOptionsBuilder<BlogAppCoreDbContext>();
            optionsBuilder.UseNpgsql(connectionString, opt => opt.MigrationsAssembly(typeof(BlogAppCoreDbContext).Assembly.FullName));

            return new BlogAppCoreDbContext(optionsBuilder.Options);
        }
    }
}