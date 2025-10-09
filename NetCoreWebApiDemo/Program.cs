using NetCoreWebApiDemo.Interfaces;
using NetCoreWebApiDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Transient
builder.Services.AddTransient<IGuidService, GuidService>();
builder.Services.AddTransient<TransientGuidService>();

// Scoped
builder.Services.AddScoped<ScopedGuidService>();

// Singleton
builder.Services.AddSingleton<SingletonGuidService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
