using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Contract.Notification
{
    public class Notification
    {
        public NotificationItem<string>[] Blobs { get; set; }
        public Notification()
        {
            this.Blobs = new NotificationItem<string>[0];
        }
    }
}
