using Api.Contract.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationMessage = Api.Contract.Notification.Notification;

namespace Client.Notification
{
    public interface INotificationService
    {
        Task Notify<T>(NotificationItem<T> item, Action<NotificationMessage, NotificationItem<T>> setter);

        Task<NotificationMessage> Receive();
    }
}
