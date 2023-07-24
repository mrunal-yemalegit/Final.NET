using Final.NetAPI.DataModels;
using Final.NetAPI.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Final.NetAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get;  }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors((options)=> 
            {
                options.AddPolicy("angularapplication", (builder) =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .WithMethods()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();



                });

                });
            services.AddControllers();
            services.AddDbContext<StudentAdminContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("StudentAdminPortalDB")));

            services.AddScoped<IstudentRepository, SqlStudentRepository>();
            services.AddScoped<IimageRepository, LocalStorageImageRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Final.NetAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Final.NetAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Resources")),
                RequestPath = "/Resources"
            });
            

            app.UseCors("angularapplication");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();


            });
        }
    }
}
