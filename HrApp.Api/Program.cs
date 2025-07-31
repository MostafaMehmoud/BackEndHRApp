using HrApp.Api.Middlewares;
using HrApp.API;

using HrApp.API.Helpers;

using HrApp.API.Repos;
using HrApp.BL.Interfaces.Helpers;
using HrApp.BL.Services;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Configratins;
using HrApp.DAL.Data;
using HrApp.DAL.Repository;
using HrApp.DAL.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace HrApp.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configration = builder.Configuration;

        builder.Services.AddControllers()
        .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {

            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            opt.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

        });

        builder.Services.Configure<AppSettingsConfig>(configration.GetSection(AppSettingsConfig.Name));
        builder.Services.Configure<BusinessSettingsConfig>(configration.GetSection(BusinessSettingsConfig.Name));
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddScoped<IJwtUtils, JwtUtils>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IVacationService, VacationService>();
        builder.Services.AddScoped<IAdvancePaymentService, AdvancePaymentService>();
        builder.Services.AddScoped<ILetterService, LetterService>();
        builder.Services.AddScoped<ICustodyService, CustodyService>();
        builder.Services.AddScoped<IPermissionService, PermissionService>();
        builder.Services.AddScoped<IUploadFileService, UploadFileService>();
        builder.Services.AddScoped<IEmployeeAttendanceService, EmployeeAttendanceService>();
        builder.Services.AddScoped<IManageService, ManageService>();
        builder.Services.AddScoped<IBranchService, BranchService>();
        builder.Services.AddScoped<ICompanyService, CompanyService>();
        builder.Services.AddDbContext<HrAppDbContext>(opt =>
        {
            opt.UseOracle(configration.GetConnectionString("HrAppDatabase"));
        });

        builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IVacationRepository, VacationRepository>();
        builder.Services.AddTransient<IAdvancePaymentRepository, AdvancePaymentRepository>();
        builder.Services.AddTransient<IUploadFileRepository, UploadFileRepository>();
        builder.Services.AddTransient<IPermissionRepository, PermissionRepository>();
        builder.Services.AddTransient<ILetterRepository, LetterRepository>();
        builder.Services.AddTransient<ICustodyRepository, CustodyRepository>();
        builder.Services.AddTransient<IEmployeeDetailsRepository, EmployeeDetailsRepository>();
        builder.Services.AddTransient<IAuthLogRepository, AuthLogRepository>();
        builder.Services.AddTransient<IEmployeeImageRepository, EmployeeImageRepository>();
        builder.Services.AddTransient<IEmployeeAttendanceRepository, EmployeeAttendanceRepository>();
        builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
        builder.Services.AddTransient<IJobService, JobService>();
        builder.Services.AddTransient<IReligionService, ReligionService>();
        builder.Services.AddTransient<INationService, NationService>();
        builder.Services.AddTransient <ICityService, CityService>();   
        builder.Services.AddTransient<ICountryService, CountryService>();   
        builder.Services.AddTransient<ICollegeService, CollegeService>();   
        builder.Services.AddTransient<INeighborService,NeighborService>();
        builder.Services.AddTransient<IKafilService,KafilService>();
        builder.Services.AddTransient<IDepartmentService, DepartmentService>(); 
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "_cors", builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin();
            });
        });



        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            
        }
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors("_cors");
        app.UseAuthentication();
        app.UseAuthorization();


        // global error handler
        app.UseMiddleware<ErrorHandlerMiddleware>();

        // custom jwt auth middleware
        app.UseMiddleware<JwtMiddleware>();
        app.MapControllers();

        app.Run();
    }
}
