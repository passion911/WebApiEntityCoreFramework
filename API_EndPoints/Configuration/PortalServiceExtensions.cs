using API_EndPoints.Api.Scopes;
using Client.Area;
using Client.Notification;
using Client.Storage;
using Client.Storage.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API_EndPoints.Configuration
{
    public static class PortalServiceExtensions
    {
        public static IServiceCollection AddMongoDatabase(
           this IServiceCollection services,
           Action<MongoClientOptions> configureAction)
        {
            services.Configure(configureAction);
            services.AddMongoDatabase();
            return services;
        }

        public static IServiceCollection AddNotificationService(
           this IServiceCollection services,
           Action<ServiceBusNotificationServiceOptions> configureAction
       )
        {
            services.Configure(configureAction);
            services.AddNotificationService();
            return services;
        }

        public static IServiceCollection AddNotificationService(this IServiceCollection services)
        {
            services.TryAddSingleton<INotificationService, BroadcastNotificationService>();            

            return services;
        }

        public static IServiceCollection ConfigureServices(
            this IServiceCollection services,
            Action<PortalOptions> configureAction
        )
        {
            services.Configure(configureAction);
            services.ConfigureServices();
            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IClock, Clock>();
            services.AddSingleton<IConfigureOptions<MongoRepositoryOptions>, PortalConfiguration>();
            services.AddSingleton<IConfigureOptions<StorageSourceOptions>, PortalConfiguration>();
            services.AddSingleton<IConfigureOptions<ServiceBusNotificationServiceOptions>, PortalConfiguration>();
            return services;
        }

        public static IServiceCollection AddMongoDatabase(this IServiceCollection services)
        {
            services.TryAddScoped<IMongoClient>(sp =>
                new MongoClient(sp.GetService<IOptions<MongoClientOptions>>().Value?.MongoConnectionString));
            services.TryAddSingleton<ICustomBsonMapper, CustomBsonMapper>();

            return services;
        }

        public static IServiceCollection AddBlobRepository(this IServiceCollection services)
        {
            // Register as singleton to avoid container initialization on each request.
            services.TryAddSingleton<ISource<string, Blob, SourceContext>, BlobRepository<SourceContext>>();
            services.TryAddSingleton<ISource<string, Blob, Blob, ScopedContext>, BlobRepository<ScopedContext>>();
            return services;
        }


    }
}
