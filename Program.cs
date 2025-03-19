using Microsoft.EntityFrameworkCore;
using Todos.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = string.Format(
    builder.Configuration.GetConnectionString("TodoConnection"),
    Environment.GetEnvironmentVariable("DB_USERNAME"),
    Environment.GetEnvironmentVariable("DB_PASSWORD")
);
builder.Services.AddDbContext<TodoContext>(opt => opt.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
