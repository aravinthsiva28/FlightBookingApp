using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AirlineService.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AirlineService.DataModel;
using AirlineService.DataModel.Interface;
using AirlineService.DataModel.Repository;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AirlineService.DataModel.Auth;
using Microsoft.IdentityModel.Tokens;

namespace AirlineService
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
            var appSettingSection = Configuration.GetSection("AppSetting");
            services.Configure<AppSetting>(appSettingSection);

            var appSetting = appSettingSection.Get<AppSetting>();
            var key = Encoding.ASCII.GetBytes(appSetting.key);

            services.AddAuthentication(au =>
            {
                au.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                au.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddDbContextPool<DataContext>(option => 
            option.UseSqlServer(Configuration.GetConnectionString("SQLConnection")));


            services.AddScoped<IAirlineServiceRepository, AirlineServiceRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IFlightShedule,FlightShedule>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddSwaggerGen();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flight Service");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").
            AllowAnyHeader().
            AllowAnyMethod());


            app.UseMvc();
            app.UseHttpsRedirection();
        }
    }
}
