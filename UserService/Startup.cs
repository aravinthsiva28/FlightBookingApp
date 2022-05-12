using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;
using UserService.RabbitMQ;

namespace UserService
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
            services.AddDbContextPool<DataContext>(option =>
            option.UseSqlServer(Configuration.GetConnectionString("SQLConnection")));

            services.AddScoped<IUserRepo,UserRepo>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(Options =>
            {
                Options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title ="Test swagger",
                    Description = "Test API",
                    Version = "V1"
                });
            });

            services.AddSingleton<IMessageSender, MessageSender>();
            
        }
            
            

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").
            AllowAnyHeader().
            AllowAnyMethod());

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(Options =>
            {
                Options.SwaggerEndpoint("/swagger/v1/swagger.json", "Test swagger");
            });

            
                //m => m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        }
    }
}
