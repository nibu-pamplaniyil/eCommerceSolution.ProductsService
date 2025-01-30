

namespace eCommerce.ProductsMicroService.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                //invoking subsequient middleware such as routing middleware
                //either in routing middleware or subsequient middleware if any expection raised we are catching that exception here 
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //Log the expection type and message
                _logger.LogError($"{ex.GetType().ToString()}:{ex.Message}");
                if (ex.InnerException != null)
                {
                    //Log the expection type and message
                    _logger.LogError($"{ex.InnerException.GetType().ToString()}:{ex.InnerException.Message}");
                }
                httpContext.Response.StatusCode = 500;//Internal Server Error
                await httpContext.Response.WriteAsJsonAsync(new { Message = ex.Message, Type = ex.GetType().ToString() });
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
