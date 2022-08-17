using Microsoft.AspNetCore.Http.Features;

namespace API_EndPoints.Configuration
{
    public class DisableRequestLimitsMiddleware
    {
        private readonly bool isDevelopment;
        private readonly RequestDelegate next;

        public DisableRequestLimitsMiddleware(
                RequestDelegate next,
                IWebHostEnvironment hostingEnvironment
            )
        {
            this.next = next;
            this.isDevelopment = hostingEnvironment.IsDevelopment();
        }

        public async Task Invoke(HttpContext context)
        {
            context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = null;
            await this.next(context);
        }
    }
}
