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
