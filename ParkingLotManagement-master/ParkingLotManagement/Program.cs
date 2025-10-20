using Microsoft.EntityFrameworkCore;
using ParkingLotManagement;
using ParkingLotManagement.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ParkingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ParkingLotAPI", Version = "v1" });
});
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddScoped<ParkingSpotsRepository>();
builder.Services.AddScoped<PricingPlansRepository>();
builder.Services.AddScoped<SubscribersRepository>();
builder.Services.AddScoped<SubscriptionsRepository>();
builder.Services.AddScoped<LogsRepository>();

var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(/*"/swagger/v1/swagger.json", "ParkingLotAPI V1"*/ "/swagger/v1/swagger.json", "ParkingLotAPI V1");
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();