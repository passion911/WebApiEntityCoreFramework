using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

namespace API_EndPoints.SwaggerExtensions
{
    public static class PreSerializeFilterExtensions
    {
        public static void AddSwaggerBehindGatewayFilter(this List<Action<OpenApiDocument, HttpRequest>> preSerializeFilters, string appGatewayPrefix)
        {
            preSerializeFilters.Add(SwaggerBehindGatewayFilter(appGatewayPrefix));
        }

        public static Action<OpenApiDocument, HttpRequest> SwaggerBehindGatewayFilter(string appGatewayPrefix)
        {
            return (doc, req) =>
            {
                string hostValue = req.Headers.ContainsKey(ForwardedHeadersDefaults.XOriginalHostHeaderName) ?
                    $"{req.Headers[ForwardedHeadersDefaults.XOriginalHostHeaderName]}{appGatewayPrefix}" :
                    req.Host.Value;
                doc.Servers = new List<OpenApiServer> { new() { Url = $"{req.Scheme}://{hostValue}" } };
            };
        }
    }
}
