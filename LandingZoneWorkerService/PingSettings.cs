using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingZoneWorkerService
{
    public class PingSettings
    {
        public TimeSpan Timeout { get; set; }
        public TimeSpan Frequency { get; set; }
        public string Target { get; set; }
    }
}
