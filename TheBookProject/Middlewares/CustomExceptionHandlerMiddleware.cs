using System.Net;
using System.Net.Mime;

namespace TheBookProject.Middlewares;

public class CustomExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;

            switch (exception)
            {
                case KeyNotFoundException e:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            await context.Response.WriteAsync($"{exception.Message}");
        }
    }
    
}