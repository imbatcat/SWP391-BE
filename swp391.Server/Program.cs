<<<<<<< Updated upstream
using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Repositories;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services;
using PetHealthcare.Server.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

const string DataSrc = "MEOMATLON\\SQLEXPRESS", Password = "MukuroHoshimiya";

// Add services to the container.
builder.Services.AddDbContext<PetHealthcareDbContext>(
option => option.UseSqlServer(
        $"Data Source={DataSrc}; User = sa; Password = {Password}; Initial Catalog=PetHealthCareSystem;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));
//Repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICageRepository, CageRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IHealthService, HealthService>();
builder.Services.AddScoped<ICageService, CageService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    }
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen().AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
=======
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
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var config = builder.Configuration;
const string DataSrc = "MSI", Password = "123456";


// Add services to the container.
#region DBcontext
builder.Services.AddDbContext<PetHealthcareDbContext>(
option => option.UseSqlServer(
        $"Data Source={DataSrc}; User = sa; Password ={Password};Initial Catalog=PetHealthCareSystem;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));
builder.Services.AddDbContext<ApplicationDbContext>(
option => option.UseSqlServer(
        $"Data Source={DataSrc}; User = sa; Password ={Password};Initial Catalog=PetHealthCareSystemAuth;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));
#endregion

#region Repositories
builder.Services.AddScoped<IServiceOrderRepository, ServiceOrderRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICageRepository, CageRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<ITimeslotRepository, TimeslotRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
builder.Services.AddScoped<IAdmissionRecordRepository, AdmissionRecordRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
#endregion

#region Services
// Application services
builder.Services.AddScoped<IServiceOrderService, ServiceOrderService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IHealthService, HealthService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<ICageService, CageService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<ITimeSlotService, TimeslotService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAdmissionRecordService, AdmissionRecordService>();
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
// Auth services
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
#endregion

#region Cookie config
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.Name = "AspNetLogin";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});
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

#region Swagger
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    }
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen().AddSwaggerGenNewtonsoftSupport();
#endregion

#region Identity
builder.Services.AddAuthentication()
    .AddGoogle(options =>
{
    options.ClientId = config["Authentication:Google:ClientId"];
    options.ClientSecret = config["Authentication:Google:ClientSecret"];
});
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>(
    options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(30);
});
#endregion 



var app = builder.Build();

//Role seeding
DataSeeder.SeedRoles(DataSrc, Password);

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
>>>>>>> Stashed changes
