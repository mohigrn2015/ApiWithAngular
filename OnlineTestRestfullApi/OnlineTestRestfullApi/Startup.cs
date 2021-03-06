using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OnlineTestRestfullApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestRestfullApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
         
        public IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("OnlineTest")));
            
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllers();
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            //});
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnlineTestRestfullApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineTestRestfullApi v1"));
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
      //.AllowAnyOrigin()
      //.AllowAnyMethod()
      //.AllowAnyHeader());
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
