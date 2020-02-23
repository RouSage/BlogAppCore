using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlogAppCore.Extensions;

namespace BlogAppCore
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure Https Redirection
            services.ConfigureHttps(_env);
            // Add AutoMapper
            services.ConfigureAutoMapper();
            // Add MediatR
            services.ConfigureMediatR();
            // Add DbContext using PostgreSQL provider
            services.ConfigureDbContext(Configuration.GetConnectionString("BlogAppCore"));
            // Add Identity
            services.ConfigureIdentity();
            // Add Mvc
            services.ConfigureMvc();
            // Add Spa static files
            services.ConfigureSpaStaticFiles();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDatabaseErrorPage();
            }
            else
            {
                // app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.ConfigureCustomExceptionMiddleware();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.ConfigureAuthentication();
            app.ConfigureRouting();
            app.ConfigureSpa(_env);
        }
    }
}
