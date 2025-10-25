using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCoreWebApiDemo.Filters
{
    public class ApiKeyAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var key = context.HttpContext.Request.Headers["X-API-KEY"];
            if (key != "my-secret-key")
            {
                context.Result = new UnauthorizedObjectResult(
                new
                {
                    success = false,
                    message = "Yetkisiz Erişim!"
                });
            }
        }
    }
}
