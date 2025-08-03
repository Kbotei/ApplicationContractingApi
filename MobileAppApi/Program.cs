using Microsoft.EntityFrameworkCore;
using MobileAppApi.Models.Db;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

//var mobileApiConnectionString = builder.Configuration.GetConnectionString("LocalMobileApiDatabaseConnection") ?? throw new InvalidOperationException("Connection string 'LocalMobileApiDatabaseConnection' not found.");
//builder.Services.AddDbContext<MobileApiContext>(options => options.UseSqlServer(mobileApiConnectionString));

app.Run();
