using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API_EndPoints.SwaggerExtensions
{
    public class TagDescriptionsDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new OpenApiTag[]
            {
                new OpenApiTag {Name = "Student", Description= "Management Apis related to Employees"}
            };
        }
    }
}
