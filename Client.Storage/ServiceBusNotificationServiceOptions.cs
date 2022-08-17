using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Storage
{
    public class ServiceBusNotificationServiceOptions
    {
        public string ServiceBusTopicName { get; set; }

        public string ServiceBusConnectionString { get; set; }
    }
}
