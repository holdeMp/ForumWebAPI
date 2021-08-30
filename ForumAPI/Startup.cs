using AutoMapper;
using Business;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using DAL;
using DAL.Interfaces;
using Data;
using Data.Entities;
using ForumAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ForumDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("ForumDB")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IMapper>(s => new Mapper(configuration));
            services.AddTransient<ISectionService, SectionService>();
            services.AddControllers().AddNewtonsoftJson();
            services.AddIdentity<User, IdentityRole>(options=>options.User.RequireUniqueEmail=true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllers();
            services.Configure<IdentityOptions>(options =>
            {

                options.Password.RequireNonAlphanumeric = false;

            });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ForumApp/dist";
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ForumApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options=>options.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                app.UseSpaStaticFiles();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ForumApi v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ForumApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "ng serve --o");
                }
            });
        }
    }
}
