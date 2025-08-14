using Microsoft.EntityFrameworkCore;
using MobileAppApi.Models.Db;
using MobileAppApi.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var mobileApiConnectionString = builder.Configuration.GetConnectionString("LocalDatabaseConnection") ?? throw new InvalidOperationException("Connection string 'LocalMobileApiDatabaseConnection' not found.");
builder.Services.AddDbContext<MobileApiContext>(options => options.UseSqlServer(mobileApiConnectionString));

builder.Services.AddScoped<ApplicationStore>();
builder.Services.AddScoped<UserStore>();

builder.Services.AddControllers();
// TODO: disable swagger in production
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
