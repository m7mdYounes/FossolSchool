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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "FosoolSchool API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter your JWT token like this: Bearer {your token}"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                       new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                       {
                          Reference = new Microsoft.OpenApi.Models.OpenApiReference
                          {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                          }
                       },
                       Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddHttpContextAccessor();


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

            #region CORS
            string cors = "";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(cors,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
            #endregion

            #region IOC
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IAuthService,AuthService>();
            builder.Services.AddScoped<ITokenService,TokenService>();
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IAcademicTermService, AcademicTermService>();
            builder.Services.AddScoped<IAcedemicTermRepo, AcademicTermRepo>();
            builder.Services.AddScoped<ILevelRepo, LevelRepo>();
            builder.Services.AddScoped<ILevelService, LevelService>();
            builder.Services.AddScoped<IGradeRepo, GradeRepo>();
            builder.Services.AddScoped<IGradeService, GradeService>();
            builder.Services.AddScoped<ISubjectService, SubjectService>();
            builder.Services.AddScoped<ISubjectRepo, SubjectRepo>();
            builder.Services.AddScoped<ILessonService, LessonService>();
            builder.Services.AddScoped<ILessonRepo, LessonRepo>();
            builder.Services.AddScoped<IStudentRepo,StudentRepo>();
            builder.Services.AddScoped<IStudentService, StudentService>();  
            builder.Services.AddScoped<ITeacherService, TeacherService>();
            builder.Services.AddScoped<ITeacherRepo, TeacherRepo>();
            builder.Services.AddScoped<IClassRepo, ClassRepo>();
            builder.Services.AddScoped<IClassService, ClassService>();
            builder.Services.AddScoped<ILessonResourceService, LessonResourceService>();
            builder.Services.AddScoped<ILessonResourceRepo, LessonResourceRepo>();

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(cors);

            app.MapControllers();

            app.Run();
        }
    }
}
