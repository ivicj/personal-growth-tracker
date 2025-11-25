using Microsoft.EntityFrameworkCore;
using PersonalGrowthTracker.Api.Infrastructure.Data;
using PersonalGrowthTracker.Api.Infrastructure.Repositories;
using PersonalGrowthTracker.Api.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

const string AllowAllOriginsPolicy = "AllowAllOrigins";

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddResponseCaching();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IMoodRepository, EfMoodRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAllOriginsPolicy, policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors(AllowAllOriginsPolicy);
app.UseResponseCaching();

// Health check endpoint
app.MapGet("/health", (HttpContext ctx) =>
{
    ctx.Response.Headers.CacheControl = "no-store, no-cache, must-revalidate";
    return Results.Ok("OK - PersonalGrowthTracker.Api is running");
});

app.MapControllers();

app.Run();


