using Api.Contract.Models;
using Api.Contract.Validation;
using Client.Area;

namespace API_EndPoints.Api.Hooks
{
    public class NotificationsSubscriptionRepository
    {
        private readonly ISource<string, NotificationSubscription, SourceContext> source;

        public NotificationsSubscriptionRepository(
                ISource<string, NotificationSubscription, SourceContext> source
            )
            => this.source = source ?? throw new APIArgumentNullException(nameof(source));

        private static string GenerateNewId()
            => Guid.NewGuid().ToString("N");

        public async Task<NotificationSubscription> Single(string id, CancellationToken cancellationToken)
        {
            SourceResult<string, NotificationSubscription> result =
                await this.source.Single(id, new SourceContext(), SourceOptions.Empty, cancellationToken);
            return result.Value;
        }

        public async Task<NotificationSubscription[]> All(CancellationToken cancellationToken)
        {
            return await this.source.All(new SourceContext(), SourceOptions.Empty, cancellationToken);
        }

        public async Task<string> Add(
                string callbackUrl,
                string topic,
                DateTime? expirationUtc,
                CancellationToken cancellationToken
            )
        {
            if (string.IsNullOrEmpty(callbackUrl))
            {
                throw new APIArgumentException(nameof(callbackUrl));
            }
            if (string.IsNullOrEmpty(topic))
            {
                throw new APIArgumentException(nameof(topic));
            }

            if ((await this.AllForTopic(topic, cancellationToken)).All(s => s.Callback != callbackUrl))
            {
                SourceResult<string, NotificationSubscription> result = await this.source.Insert(
                        new NotificationSubscription()
                        {
                            Id = GenerateNewId(),
                            Topic = topic,
                            Callback = callbackUrl,
                            IsEnabled = true,
                            ExpirationUtc = expirationUtc
                        },
                        new SourceContext(),
                        SourceOptions.Empty,
                        cancellationToken
                    );

                result.ThrowIfNotSuccess();
                return result.Id;
            }
            else
            {
                throw new SourceException(SourceResultCode.Conflict);
            }
        }

        public async Task<bool> Remove(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new APIArgumentException(nameof(id));
            }

            SourceResult<string, NotificationSubscription> result = await this.source.Remove(
                    id,
                    new SourceContext(),
                    SourceOptions.Empty,
                    cancellationToken
                );

            return result.IsSuccess;
        }

        private async Task<bool> Update(string id, NotificationSubscription entity, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new APIArgumentException(nameof(id));
            }

            if (entity == null)
            {
                throw new APIArgumentNullException(nameof(entity));
            }

            SourceResult<string, NotificationSubscription> result =
                await this.source.Update(id, entity, new SourceContext(), SourceOptions.Empty, cancellationToken);
            return result.IsSuccess;
        }

        public async Task<NotificationSubscription[]> AllForTopic(string topic, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(topic))
            {
                throw new APIArgumentException(nameof(topic));
            }

            return await this.source.All(a => a.Topic == topic, new SourceContext(), SourceOptions.Empty, cancellationToken);
        }

        public async Task<bool> Update(
                string id,
                Action<NotificationSubscription> modifyAction,
                CancellationToken cancellationToken
            )
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new APIArgumentException(nameof(id));
            }

            if (modifyAction == null)
            {
                throw new APIArgumentNullException(nameof(modifyAction));
            }

            SourceResult<string, NotificationSubscription> result =
                await this.source.Single(id, new SourceContext(), SourceOptions.Empty, cancellationToken);

            if (result.IsSuccess && result.Value != null)
            {
                NotificationSubscription entity = result.Value;
                modifyAction(entity);
                return await this.Update(id, entity, cancellationToken);
            }
            else
            {
                return false;
            }
        }
    }
}
