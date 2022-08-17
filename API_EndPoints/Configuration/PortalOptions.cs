namespace API_EndPoints.Configuration
{
    public class PortalOptions
    {
        public string MongoDatabaseName { get; set; }

        public TimeSpan? SlidingExpiration { get; set; }

        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; } = TimeSpan.FromMinutes(10);

        public string ServiceBusTopicName { get; set; }

        public string StorageAccountConnectionString { get; set; }

        public string BlobContainerName { get; set; }

        public string TableNamePrefix { get; set; }
    }
}
