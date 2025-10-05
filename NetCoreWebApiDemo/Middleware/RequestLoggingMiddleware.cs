namespace NetCoreWebApiDemo.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
            var start=DateTime.Now;

            await _next(context);

            var elapsed=DateTime.Now-start;
            Console.WriteLine($"Istek: {path} | Sure:{elapsed.TotalMilliseconds} ms");
        }
    }
}
