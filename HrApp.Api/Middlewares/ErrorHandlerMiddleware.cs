
using HrApp.API.Helpers;
using HrApp.BL.Helpers;
using System.Net;
using System.Text.Json;

namespace HrApp.Api.Middlewares;
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            _logger.LogError(error, error.Message);

            // تخصيص الأخطاء بناءً على نوع الاستثناء
            switch (error)
            {
                case AppException e:
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity; // 422
                    break;
                case KeyNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound; // 404
                    break;
                case UnauthorizedAccessException e:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500
                    break;
            }

            // يمكنك إضافة مزيد من التفاصيل للخطأ مثل stack trace
            var result = JsonSerializer.Serialize(new
            {
                message = error?.Message,
                details = error?.StackTrace // قد تساعدك في التحليل أثناء التطوير
            });

            await response.WriteAsync(result);
        }
    }

}