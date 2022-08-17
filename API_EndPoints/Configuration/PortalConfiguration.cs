using Api.Contract.Validation;
using Client.Storage;
using Microsoft.Extensions.Options;

namespace API_EndPoints.Configuration
{
    public class PortalConfiguration : IConfigureOptions<CacheOptions>,
                                       IConfigureOptions<MongoRepositoryOptions>,
                                       IConfigureOptions<StorageSourceOptions>,
                                       IConfigureOptions<ServiceBusNotificationServiceOptions>
    {
        private readonly PortalOptions portalOptions;
        private readonly IConfiguration configuration;
        public PortalConfiguration(IOptions<PortalOptions> options, IConfiguration configuration)
        {
            if (options == null)
            {
                throw new APIArgumentNullException(nameof(options));
            }

            portalOptions = options.Value;
            this.configuration = configuration ?? throw new APIArgumentNullException(nameof(configuration));
        }

        public void Configure(StorageSourceOptions options)
        {
            options.ConnectionString = this.configuration.GetConnectionString("StorageAccountConnectionString");
            options.ContainerName = this.portalOptions.BlobContainerName;
            options.TableNamePrefix = portalOptions.TableNamePrefix;
        }

        public void Configure(CacheOptions options)
        {
            options.AbsoluteExpirationRelativeToNow = this.portalOptions.AbsoluteExpirationRelativeToNow;
            options.SlidingExpiration = this.portalOptions.SlidingExpiration;
        }

        public void Configure(MongoRepositoryOptions options)
        {
            options.DatabaseName = portalOptions.MongoDatabaseName;
        }

        public void Configure(ServiceBusNotificationServiceOptions options)
        {
            options.ServiceBusTopicName = this.portalOptions.ServiceBusTopicName;
        }
    }
}
