namespace Robolain.WebApi.Middleware
{
    public static class MiddlewareRegister
    {
        public static IApplicationBuilder UseExceptionValidationMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionValidationMiddleware>();
            return app;
        }
    }
}
