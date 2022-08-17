namespace API_EndPoints.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder DisableRequestLimits(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DisableRequestLimitsMiddleware>();
        }
    }
}
