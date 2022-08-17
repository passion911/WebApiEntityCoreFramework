using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Contract.Notification
{
    public abstract class NotificationItemBase
    {
        public NotificationType Type { get; protected set; }

        public Topic Topic { get; protected set; }
    }
}
