using CorrelationId;
using CorrelationId.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApiDemo;
using NetCoreWebApiDemo.Middleware;
using NetCoreWebApiDemo.Models;
using NetCoreWebApiDemo.Profiles;
using NetCoreWebApiDemo.Repository;
using NetCoreWebApiDemo.Services;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .Enrich.WithCorrelationId()
    .WriteTo.Console(outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] [CID:{CorrelationId}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File("Logs/app-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


builder.Host.UseSerilog();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

builder.Services.AddControllers();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var env = builder.Environment;
builder.Configuration.AddJsonFile($"appsettings.{env.EnvironmentName}.json",optional:true,reloadOnChange:true);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

var config = builder.Configuration;
string connection = config.GetConnectionString("DefaultConnection")??"";

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDefaultCorrelationId(options =>
{
    options.AddToLoggingScope = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCorrelationId();
app.UseMiddleware<ExceptionMiddleware>();

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();
