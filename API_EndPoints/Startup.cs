using Api.Contract.Converter;
using API_EndPoints.Configuration;
using API_EndPoints.SwaggerExtensions;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

namespace API_EndPoints
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //TODO
            services.AddApplicationInsightsTelemetry();
            services.AddSwaggerGen(c =>
            {
                c.DescribeAllParametersInCamelCase();
                c.DocumentFilter<TagDescriptionsDocumentFilter>();
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "King Kong API",
                    Version = "v1",
                    Description = GetEmbeddedResource("API_EndPoints.SwaggerDocs", "DocDescriptionV1.md")
                });
                c.EnableAnnotations();
            });
            services.AddMvc(c =>
            {
                c.Filters.Add<TranslateBodyResultServiceFilter>();
                c.EnableEndpointRouting = false;
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals;
                options.JsonSerializerOptions.Converters.Add(new RestrictedStringEnumConverterFactory());
                options.JsonSerializerOptions.Converters.Add(new StringNullableGuidConverter());
                options.JsonSerializerOptions.Converters.Add(new StringNullableIntConverter());
                options.JsonSerializerOptions.Converters.Add(new StringNullableDecimalConverter());
                options.JsonSerializerOptions.Converters.Add(new LongToStringConverter());
            });

            services.ConfigureServices(this.Configuration.Bind)
                .AddMemoryCache(options =>
                {
                    options.ExpirationScanFrequency = TimeSpan.Parse(Configuration.GetValue<string>("AbsoluteExpirationRelativeToNow"));
                })
                .AddMongoDatabase(this.Configuration.GetSection("ConnectionStrings").Bind)
                .AddNotificationService(this.Configuration.GetSection("ConnectionStrings").Bind)
                .AddBlobRepository();

            services.AddControllers();
            services.AddAuthorization();
            services.AddAuthentication();
            services.AddResponseCompression();
        }

        public void Configure(WebApplication app)
        {
            app.DisableRequestLimits();
            if (Environment.IsProduction())
            {
                app.UseHsts();
            }

            app.UseDeveloperExceptionPage();
            app.UseCookiePolicy();
            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.AddSwaggerBehindGatewayFilter(Configuration.GetValue<string>("AppGwPrefix"));
                //c.RouteTemplate = "/swagger/{document}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "King Kong Hub");
                c.RoutePrefix = "swagger";
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvcWithDefaultRoute();
        }

        private string GetEmbeddedResource(string ns, string res)
        {
            StreamReader a = new(Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("{0}.{1}", ns, res)));
            using (StreamReader reader = new(Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("{0}.{1}", ns, res))))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
