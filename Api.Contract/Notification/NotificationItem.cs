using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Contract.Notification
{
    public class NotificationItem<TId> : NotificationItemBase
    {
        public TId Id { get; }

        public NotificationItem(TId id, NotificationType type, Topic topic)
        {
            this.Id = id;
            this.Type = type;
            this.Topic = topic;
        }

        public NotificationItem<dynamic> ToDynamicIdObject()
        {
            return new NotificationItem<dynamic>(
             Id,
             Type,
             Topic
            );
        }
    }
}
