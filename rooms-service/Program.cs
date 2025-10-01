using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// CORS (solo para pruebas locales)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:8080")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowLocalFrontend");

app.MapGet("/health", () => Results.Ok(new { status = "rooms ok" }));

app.MapGet("/rooms", () =>
{
    var rooms = new[] {
        new { Id = 1, Name = "Habitación A", Price = 250000 },
        new { Id = 2, Name = "Habitación B", Price = 300000 }
    };
    return Results.Ok(rooms);
});

app.Run();
