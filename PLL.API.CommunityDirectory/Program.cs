using BLL.CommunityDirectory.Context;
using BLL.CommunityDirectory.Repositories;
using DAL.CommunityDirectory.Interfaces;
using DAL.CommunityDirectory.Models.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SLL.CommunityDirectory.Interfaces;
using SLL.CommunityDirectory.Mapping;
using SLL.CommunityDirectory.Services;
using System.Text;
using AutoMapper;
namespace PLL.API.CommunityDirectory;

public class Program
{
    // FIX: Changed 'void' to 'Task'
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        //AutoMapper Configuration
        //builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



        // 1. Controllers & JSON Options
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

        // 2. Swagger & JWT Security
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Community Directory API", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    Array.Empty<string>()
                }
            });
        });

        // 3. Database Configuration
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // 4. Identity Configuration
        builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        // 5. JWT Authentication
        var jwtSecret = builder.Configuration["JWT:Secret"] ?? "FALLBACK_Key_12345678901234567890";
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
            };
        });

        // 6. CORS (Only need one definition)
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngular", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        // 7. Dependency Injection
        builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
        builder.Services.AddScoped<IResourceServices, ResourceService>();
        builder.Services.AddScoped<IEventRepository, EventRepository>();
        builder.Services.AddScoped<IEventServices, EventService>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ICategoryServices, CategoryService>();

        var app = builder.Build();

        // 8. Seeding Roles
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        app.MapDefaultEndpoints();

        // 9. Middleware Pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowAngular");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // FIX: Use RunAsync for async Main
        await app.RunAsync();
    }
}