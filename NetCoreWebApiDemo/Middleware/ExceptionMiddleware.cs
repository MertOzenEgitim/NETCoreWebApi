using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiDemo.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace NetCoreWebApiDemo.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // sonraki middleware'lere devam et
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred! TraceId: {TraceId}", context.TraceIdentifier);

                var problem = CreateProblemDetails(ex, context);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = problem.Status ?? (int)HttpStatusCode.InternalServerError;

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                await context.Response.WriteAsync(JsonSerializer.Serialize(problem, options));
            }
        }

        private ProblemDetails CreateProblemDetails(Exception ex, HttpContext context)
        {
            var problem = new ProblemDetails
            {
                Instance = context.Request.Path,
                Detail = _env.IsDevelopment() ? ex.ToString() : "Sunucu hatası oluştu.",
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Sunucu Hatası",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
            };

            problem.Extensions["traceId"] = context.TraceIdentifier;

            if (ex is NotFoundException)
            {
                problem.Status = (int)HttpStatusCode.NotFound;
                problem.Title = "Kaynak bulunamadı";
            }

            return problem;
        }

    }
}
