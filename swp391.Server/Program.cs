using Microsoft.EntityFrameworkCore;
using PetHealthcare.Server.Repositories;
using PetHealthcare.Server.Repositories.Interfaces;
using PetHealthcare.Server.Services;
using PetHealthcare.Server.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Define constants and variables
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
const string DataSrc = "MEOMATLON\\SQLEXPRESS", Password = "MukuroHoshimiya";

// Add services to the container
builder.Services.AddDbContext<PetHealthcareDbContext>(options =>
    options.UseSqlServer($"Data Source={DataSrc}; Database=PetHealthCareSystem; User ID=sa;Password={Password};Connect Timeout=30; Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite; Multi Subnet Failover=False"));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICageRepository, CageRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICageService, CageService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("https://localhost:5173").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers()
   .AddNewtonsoftJson(options =>
   {
       options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
   });

// Swagger/OpenAPI configurations
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen().AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}

app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Controller mapping
app.MapControllers();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller}/{action=Index}/{id?}");

//app.MapFallbackToFile("index.html");

app.Run();
