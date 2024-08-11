
using MediatR;

namespace APIGateway.Api.Middlewares;

public class MiddlewareRequest : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.TraceIdentifier = Guid.NewGuid().ToString();

        Console.WriteLine(nameof(MiddlewareRequest));

        return next(context);
    }
}
