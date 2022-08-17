using Api.Contract.Notification;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationMessage = Api.Contract.Notification.Notification;

namespace Client.Notification
{
    public class BroadcastNotificationService : INotificationService
    {
        private readonly ConcurrentQueue<NotificationMessage> _requests = new();

        public async Task<NotificationMessage> Receive()
        {
            _requests.TryDequeue(out var result);
            return await Task.FromResult(result);
        }

        public Task Notify<T>(NotificationItem<T> item, Action<NotificationMessage, NotificationItem<T>> setter)
        {
            var notification = new NotificationMessage();
            setter(notification, item);

            _requests.Enqueue(notification);
            return Task.CompletedTask;
        }
    }
}
