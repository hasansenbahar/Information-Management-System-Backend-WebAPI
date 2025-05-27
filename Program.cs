using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Configuration;
using System.Reflection;
using WebService.API.Auth;
using WebService.API.Authorization;
using WebService.API.Data;
using WebService.API.Data.Entity;
using WebService.API.Middleware;
using WebService.API.Properties;
using WebService.API.Services.Contracts;
using WebService.API.Services.IServices;
using WebService.API.Services.Repository;

internal class Program
{
   
    private static void Main(string[] args)
    {
        

        var builder = WebApplication.CreateBuilder(args);
        var _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

        // Add services to the container.
        //Resgitering AutoMapper
        builder.Services.AddAutoMapper(typeof(Program));

        ////Normal User DbContext For Testing(SQL SERVER)
        //builder.Services.AddDbContext<ApplicationDbContext>();
        //Identity User DbContext for Production(SQL SERVER)

        builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

        //builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

        builder.Services.AddDbContext<WebAPIDb>();
      
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        //Registering Identity 
        builder.Services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredLength = 1;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric= false;

        }).AddEntityFrameworkStores<WebAPIDb>()
        .AddDefaultTokenProviders();

        //Registering Mail Service
        builder.Services.Configure<MailSettings>(_config.GetSection("MailSettings"));

        //Registering Interface
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IMailRepository, MailRepository>();
        builder.Services.AddTransient<IPersonRepository, PersonRepository>();

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
        });


        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        //Swagger from documentation
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Web API Db",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
            });
        });

        //Authentication from documentation
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            option.SaveToken = true;
            option.TokenValidationParameters = new TokenValidationParameters
            {
                SaveSigninToken = true,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"],       // Jwt:Issuer - config value 
                ValidAudience = _config["Jwt:Issuer"],     // Jwt:Issuer - config value 
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"])) // Jwt:Key - config value 
            };
        });
        //logging
        var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);



        //Authorization

        var app = builder.Build();

        
        //using (var scope = app.Services.CreateScope())
        //{
        //    var db = scope.ServiceProvider.GetRequiredService<WebAPIDb>();

        //    db.Database.Migrate();
            
        //}

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();


        app.UseMiddleware(typeof(ExceptionHandlingMiddleware));


        app.Run();
    }
}