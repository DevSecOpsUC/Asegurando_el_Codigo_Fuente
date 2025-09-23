using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new { status = "auth-service running" }));

// Example protected endpoint (requires Authorization header)
app.MapGet("/me", (HttpContext ctx) =>
{
    // In a real implementation validate JWT and return claims
    if (!ctx.Request.Headers.ContainsKey("Authorization"))
        return Results.Unauthorized();

    return Results.Ok(new { user = "demo-user", role = "user" });
});

app.Run();
