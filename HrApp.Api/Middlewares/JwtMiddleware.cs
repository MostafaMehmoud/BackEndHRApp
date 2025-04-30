
using HrApp.BL.Interfaces.Helpers;
using HrApp.DAL.Configratins;
using Microsoft.Extensions.Options;

namespace HrApp.Api.Middlewares;
public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettingsConfig _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettingsConfig> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var (employeeId, isAdmin) = jwtUtils.ValidateJwtToken(token);
        if (employeeId != null)
        {
            // attach Employee to context on successful jwt validation
            context.Items["EmployeeId"] = employeeId;
            context.Items["IsAdmin"] = isAdmin;
        }

        await _next(context);
    }
}