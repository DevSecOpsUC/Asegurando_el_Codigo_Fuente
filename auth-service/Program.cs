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

app.MapGet("/health", () => Results.Ok(new { status = "auth ok" }));

app.MapPost("/login", async (HttpContext ctx) =>
{
    var body = await System.Text.Json.JsonSerializer.DeserializeAsync<LoginRequest>(ctx.Request.Body);
    if (body is not null && body.User == "test" && body.Password == "test")
    {
        return Results.Ok(new { token = "fake-jwt-token" });
    }
    return Results.Unauthorized();
});

app.Run();

internal record LoginRequest(string User, string Password);
