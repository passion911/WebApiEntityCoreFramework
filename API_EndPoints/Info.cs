using Microsoft.OpenApi.Models;

namespace API_EndPoints
{
    internal class Info : OpenApiInfo
    {
        public string title { get; set; }
        public string version { get; set; }
    }
}