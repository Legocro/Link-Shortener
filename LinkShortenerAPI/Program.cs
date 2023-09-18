using LinkShortenerAPI.Models;
using LinkShortenerAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ShortLinkModelContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Dev"));
});
builder.Services.AddScoped<IShortUrl, ShortUrl>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:8080")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithExposedHeaders("Location", "location");
});
app.UseAuthorization();

app.MapControllers();

app.Run();
