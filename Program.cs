using FosoolSchool.Models;
using FosoolSchool.Models.DBContext;
using FosoolSchool.Repository;
using FosoolSchool.Repository.Interfaces;
using FosoolSchool.Services;
using FosoolSchool.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace FosoolSchool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            #region Services
            //Json Seralization
            builder.Services.AddControllers()
                        .AddJsonOptions(options =>
                        {
                            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        });
            //DB IOC 
            builder.Services.AddDbContext<FossolDB>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CN")).UseLazyLoadingProxies();
            });

            //JWT Token
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                        };
                    });

            #endregion



            #region IOC
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IAuthService,AuthService>();
            builder.Services.AddScoped<ITokenService,TokenService>();
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            //builder.Services.AddScoped<>

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
