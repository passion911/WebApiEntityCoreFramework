using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingZoneWorkerService
{
    public class PingerService : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public PingerService(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Start execute PingerService..... at :" + DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
               
        }
    }
}
