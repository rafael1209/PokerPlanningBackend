﻿using Microsoft.OpenApi.Models;
using PokerPlanningBackend.Database;
using PokerPlanningBackend.Interfaces;
using PokerPlanningBackend.Repositories;
using PokerPlanningBackend.Services;

namespace PokerPlanningBackend
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });

            services.AddHttpContextAccessor();

            services.AddSingleton<MongoDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITempUserRepository, TempUserRepository>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddScoped<IUsuallyAuthService, UsuallyAuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(k => { k.WithMethods("POST", "GET", "PATCH", "PUT"); k.AllowAnyOrigin(); k.AllowAnyHeader(); });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute().AllowAnonymous();
                endpoints.MapSwagger();
                endpoints.MapControllers().AllowAnonymous();
            });
        }
    }
}