using Api.Contract.Models;
using Client.Area;
using Client.Storage.Models;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Linq.Expressions;

namespace Client.Storage
{
    public class BlobRepository<TContext> : IBlobRepository<TContext>
    {
        private CloudBlobContainer container;
        private readonly string containerName;
        private readonly CloudBlobClient blobClient;

        public BlobRepository(IOptions<StorageSourceOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(options.Value.ConnectionString);

            //This class should be registered as singleton to avoid container initialization on each request.
            this.blobClient = storageAccount.CreateCloudBlobClient();

            this.containerName = options.Value?.ContainerName;
            if (string.IsNullOrEmpty(this.containerName))
            {
                throw new ArgumentException($"{nameof(StorageSourceOptions.ContainerName)} cannot be null or empty.",
                    nameof(options));
            }
        }

        public async Task<SourceResult<string, Blob>> Single(
              string id,
              TContext context,
              SourceOptions options,
              CancellationToken token
          )
        {
            var (bytes, contenType) = await this.Get(id);

            return bytes == null
                ? SourceResult<string, Blob>.NotFound(id)
                : SourceResult<string, Blob>.Ok(id, new Blob { Name = id, Data = bytes, Size = bytes.LongLength, ContentType = contenType });
        }

        public async Task<Blob[]> All(TContext context, SourceOptions options, CancellationToken token)
            => await this.List();

        public async Task<Blob[]> All(
                Expression<Func<Blob, bool>> filter,
                TContext context,
                SourceOptions options,
                CancellationToken token
            )
            => (await this.List()).Where(filter.Compile()).ToArray();

        public async Task<SourceResult<string, Blob>> Remove(
                string id,
                TContext context,
                SourceOptions options,
                CancellationToken token
            )
            => await this.Delete(id)
                ? SourceResult<string, Blob>.Removed(id, value: new Blob { Name = id })
                : SourceResult<string, Blob>.NotFound(id);

        public async Task<SourceResult<string, Blob>> Insert(
                Blob entity,
                TContext context,
                SourceOptions options,
                CancellationToken token
            )
        {
            await this.Write(entity.Name, entity.Data, entity.ContentType);
            return SourceResult<string, Blob>.Inserted(entity.Name, entity);
            //throw new NotSupportedException($"Please use the {nameof(this.Update)} method instead.");
        }

        public async Task<SourceResult<string, Blob>> Update(
                string id,
                Blob entity,
                TContext context,
                SourceOptions options,
                CancellationToken token
            )
        {
            await this.Write(id, entity.Data, entity.ContentType);
            return SourceResult<string, Blob>.Updated(id, entity);
        }

        public async Task<string> GetBlobSasUri(string blobName)
        {
            await EnsureInit();

            var adHocSAS = new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                Permissions = SharedAccessBlobPermissions.Read
            };

            var sasContainerToken = container.GetSharedAccessSignature(adHocSAS);

            return container.Uri + "/" + blobName + sasContainerToken;
        }

        public async Task<BlobOptions> GetBlobOptions()
        {
            await EnsureInit();

            var adHocSAS = new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                Permissions = SharedAccessBlobPermissions.Read
            };

            var sasContainerToken = container.GetSharedAccessSignature(adHocSAS);

            return new BlobOptions()
            {
                BaseUri = container.Uri,
                SharedAccessSignature = sasContainerToken
            };
        }

        private async Task Write(string blobName, byte[] bytes, string contentType)
        {
            await this.EnsureInit();
            bytes = bytes ?? new byte[0];
            CloudBlockBlob blockBlob = this.container.GetBlockBlobReference(blobName);
            if (bytes.Length == 0)
            {
                await this.Delete(blobName);
                return;
            }

            if (!string.IsNullOrEmpty(contentType))
            {
                blockBlob.Properties.ContentType = contentType;
            }

            await blockBlob.UploadFromByteArrayAsync(bytes, index: 0, bytes.Length);
        }

        private async Task<Blob[]> List()
        {
            await this.EnsureInit();
            var result = new List<Blob>();
            BlobContinuationToken token = null;
            do
            {
                BlobResultSegment resultSegment = await this.container.ListBlobsSegmentedAsync(token);
                token = resultSegment.ContinuationToken;

                foreach (IListBlobItem item in resultSegment.Results)
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        var blob = (CloudBlockBlob)item;
                        // Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);

                        result.Add(new Blob() { Name = blob.Name, Size = blob.Properties.Length });
                    }
                    else if (item.GetType() == typeof(CloudPageBlob))
                    {
                        var pageBlob = (CloudPageBlob)item;

                        //Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);
                        result.Add(new Blob() { Name = pageBlob.Name, Size = pageBlob.Properties.Length });
                    }
                }
            } while (token != null);

            return result.ToArray();
        }

        private async Task<(byte[], string)> Get(string blobName)
        {
            await this.EnsureInit();
            CloudBlockBlob blockBlob = this.container.GetBlockBlobReference(blobName);
            if (!await blockBlob.ExistsAsync())
            {
                return (null, null);
            }

            using (var stream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(stream);
                return (stream.ToArray(), blockBlob.Properties.ContentType);
            }
        }


        private async Task<bool> Delete(string blobName)
        {
            await this.EnsureInit();
            CloudBlockBlob blockBlob = this.container.GetBlockBlobReference(blobName);
            if (await blockBlob.ExistsAsync())
            {
                await blockBlob.DeleteAsync();
                return true;
            }

            return false;
        }

        private async Task EnsureInit()
        {
            if (this.container == null)
            {
                this.container = this.blobClient.GetContainerReference(this.containerName);
                await this.container.CreateIfNotExistsAsync();
            }
        }
    }

    public interface IBlobRepository<TContext> : ISource<string, Blob, TContext>
    {
        Task<string> GetBlobSasUri(string blobName);

        Task<BlobOptions> GetBlobOptions();
    }
}
