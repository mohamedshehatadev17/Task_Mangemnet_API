using System;
using System.Security.Claims;
using System.Text;
using Application;
using Asp.Versioning;
using FluentValidation;
using Infrastructure;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskMangement.API.Middlewares;
using TaskMangement.Application.Abstractions.Authentication;
using TaskMangement.Domain.Models;
using TaskMangement.Infrastructure.Authentication;
using TaskMangement.Infrastructure.Persistance.Authentication.Seed;
using TaskMangement.Infrastructure.Persistance.Contexts;

namespace TaskMangement.API
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Controllers
            builder.Services.AddControllers();

            // Open API
            builder.Services.AddOpenApi();

            // FluentValidation
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            // Clean Architecture Layers
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            // Database
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // Identity + Roles
            builder.Services
                .AddIdentity<User, IdentityRole<Guid>>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // JWT Generator
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            // Authentication
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer =
                                builder.Configuration["Jwt:Issuer"],

                            ValidAudience =
                                builder.Configuration["Jwt:Audience"],

                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Convert.FromBase64String(
                                        builder.Configuration["Jwt:Key"]!)),

                            RoleClaimType = ClaimTypes.Role
                        };

                    options.MapInboundClaims = false;
                });

            builder.Services
                .AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);

                    options.AssumeDefaultVersionWhenUnspecified = true;

                    options.ReportApiVersions = true;

                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                .AddMvc(); // 🔥 REQUIRED for controllers

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Seed Roles
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                await RoleSeeder.SeedAsync(roleManager);
            }

            // Swagger/OpenAPI
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // Middlewares
            app.UseHttpsRedirection();
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            // Controllers
            app.MapControllers();

            app.Run();
        }
    }
}
