namespace MiddlewareDemo
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string CorrelationIdHeaderName = "X-Correlation-Id";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers[CorrelationIdHeaderName].FirstOrDefault();
            if (string.IsNullOrEmpty(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }
            context.TraceIdentifier = correlationId;
            context.Request.Headers.TryAdd(CorrelationIdHeaderName, correlationId);
            context.Response.Headers.TryAdd(CorrelationIdHeaderName, correlationId);
            await _next(context);
        }
    }

    public static class CorrelationIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationIdMiddleware>();
        }
    }
}
