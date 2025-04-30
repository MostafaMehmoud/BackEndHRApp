

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HrApp.Api.Attributes;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly RoleEnum _role;

    public AuthorizeAttribute(RoleEnum role = RoleEnum.None)
    {
        _role = role ;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        // authorization
        var emplyeeId = context.HttpContext.Items["EmployeeId"];
         Boolean.TryParse(context.HttpContext.Items["IsAdmin"]?.ToString(),out bool isAdmin);
        var allowAdmin = (_role == RoleEnum.Admin) ? (isAdmin == true)?true:false : true;
        if (emplyeeId == null || !allowAdmin)
        {
            // not logged in or role not authorized
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}