using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCoreWebApiDemo.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var errorResponse = new
            {
                success = false,
                message = "Beklenmeyen bir hata oluştu",
                detail = context.Exception.Message
            };

            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }
}
