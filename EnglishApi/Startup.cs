using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using AutoMapper;
using English.Database.Data;
using English.Database.Data.Interfaces;
using English.Database.Data.Repositories;
using English.Services.Interfaces;
using English.Services;
using English.Services.Mappings;
using EnglishApi.Logger;
using EnglishApi.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using ValidationContext = AutoMapper.ValidationContext;

namespace EnglishApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
                "/nlog.config"));

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddDbContext<EnglishContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("EnglishConnection")));

            var passwordHashSettings = Configuration.GetSection("AppSettings:PasswordHashSettings").Get<PasswordHashSettings>() ?? new PasswordHashSettings();
            services.AddTransient(t => new PasswordHashSettings());
            services.AddScoped(s => new PasswordHashSettings(passwordHashSettings));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,

                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });


            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IWordDictionaryService, WordDictionaryService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<ISubsectionService, SubsectionService>();
            services.AddScoped<IUserService,UserService>();


            services.AddScoped<ILoggerManager, LoggerManager>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);






            services.AddControllersWithViews();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }else
            {
                app.UseHsts();
            }

           
            app.UseHttpsRedirection();
            app.UseStaticFiles();
           
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseCors("AllowAll");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnglishApi");
            });
            
        }
        private static void ValidateAppSettings(PasswordHashSettings passwordHashSettings)
        {
            var resultsValidation = new List<ValidationResult>();

            Validator.TryValidateObject(passwordHashSettings, new System.ComponentModel.DataAnnotations.ValidationContext(passwordHashSettings), resultsValidation, true);
            resultsValidation.ForEach(error => Console.WriteLine(error.ErrorMessage));

        }
    }
}
