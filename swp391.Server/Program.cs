using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Models.ApplicationModels;
using PetHealthcare.Server.Repositories;
using PetHealthcare.Server.Repositories.DbContext;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services;
using PetHealthcare.Server.Services.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using PetHealthcare.Server.Helpers;
using Microsoft.Extensions.DependencyInjection;
using PetHealthcare.Server.Services.AuthInterfaces;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

const string DataSrc = "MEOMATLON\\SQLEXPRESS", Password = "MukuroHoshimiya";

// Add services to the container.
builder.Services.AddDbContext<PetHealthcareDbContext>(
option => option.UseSqlServer(
        $"Data Source={DataSrc}; User = sa; Password ={Password};Initial Catalog=PetHealthCareSystem;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));


builder.Services.AddDbContext<ApplicationDbContext>(
option => option.UseSqlServer(
        $"Data Source={DataSrc}; User = sa; Password ={Password};Initial Catalog=PetHealthCareSystemAuth;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));



#region Repositories
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICageRepository, CageRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<ITimeslotRepository, TimeslotRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
#endregion

#region Services
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IHealthService, HealthService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<ICageService, CageService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<ITimeSlotService, TimeslotService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
#endregion



#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:5173")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                      });
});
#endregion
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    }
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>(
    options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen().AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();
DataSeeder.SeedRoles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();
//app.MapIdentityApi<ApplicationUser>();

app.Run();
