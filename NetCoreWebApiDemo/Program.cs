using Microsoft.EntityFrameworkCore;
using NetCoreWebApiDemo;
using NetCoreWebApiDemo.Filters;
using NetCoreWebApiDemo.Middleware;
using NetCoreWebApiDemo.Profiles;
using NetCoreWebApiDemo.Repository;
using NetCoreWebApiDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<GlobalExceptionFilter>();
//});
//builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

builder.Services.AddScoped<ApiKeyAuthorizationFilter>();
builder.Services.AddScoped<ResourceLogFilter>();
builder.Services.AddScoped<ActionLogFilter>();
builder.Services.AddScoped<WrapResponseFilter>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
