using BlogService.Data;
using BlogService.Filter.Exception;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Authentication.Token= "81SfreDnImra236d8ec201df516d0f6472d516d72d";
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             var connection = @"Server=db;Database=student123;User=sa;Password=arm!nder1;";

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
            });
            //services.AddCors(options => {
            //    options.AddPolicy("AllowSpecificOrigin", builder =>
            //    {
            //        builder.WithOrigins("http://localhost:4200");
            //        //builder.WithHeaders("")
            //    });
            //         });
            services.AddDbContext<SchoolContext>(options =>
               options.UseSqlServer(connection));
            services.AddDbContext<EmployeeContext>(options =>
               options.UseSqlServer(connection));
        //        services.AddIdentity<ApplicationUser, IdentityRole>()
        // .AddEntityFrameworkStores<ApplicationDbContext>()
        // .AddDefaultTokenProviders();
            services.AddMvc(
                config =>
                {
                    config.Filters.Add(typeof(CustomExceptionFilter));
                }
                    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAllHeaders");
            app.UseApiKey();
            app.UseMvc();
        }
    }
}
