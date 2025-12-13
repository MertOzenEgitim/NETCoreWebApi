using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using NetCoreWebApiDemo.Authorization.Requirement;
using System.Security.Claims;

public class SameCompanyHandler
    : AuthorizationHandler<SameCompanyRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SameCompanyHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        SameCompanyRequirement requirement)
    {
        var userCompanyId = context.User.FindFirst("companyId")?.Value;
        if (userCompanyId == null)
            return Task.CompletedTask;

        var routeCompanyId = _httpContextAccessor.HttpContext?
            .Request.RouteValues["companyId"]?.ToString();

        if (routeCompanyId == null)
            return Task.CompletedTask;

        if (userCompanyId == routeCompanyId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
