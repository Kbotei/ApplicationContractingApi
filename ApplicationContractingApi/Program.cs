using ApplicationContractingApi.Models.Db;
using ApplicationContractingApi.Stores;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var apiConnectionString = builder.Configuration.GetConnectionString("LocalDatabaseConnection") ?? throw new InvalidOperationException("Connection string 'LocalDatabaseConnection' not found.");
builder.Services.AddDbContext<ApplicationContractingApiContext>(options => options.UseSqlServer(apiConnectionString));

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
