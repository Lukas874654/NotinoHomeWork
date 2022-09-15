using NotinoHomeWork.Application.Exceptions;
using System.Net;

namespace NotinoHomeWork.Api;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case ItemNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case BadRequestException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            Console.WriteLine(error.ToString());

            var result = error.Message;
            await response.WriteAsync(result);
        }
    }
}
