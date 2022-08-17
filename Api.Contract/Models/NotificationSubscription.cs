using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Contract.Models
{
    public class NotificationSubscription
    {
        public string Id { get; set; }

        public string Callback { get; set; }

        public string Topic { get; set; }

        public DateTime? ExpirationUtc { get; set; }

        public bool IsEnabled { get; set; }
    }
}
