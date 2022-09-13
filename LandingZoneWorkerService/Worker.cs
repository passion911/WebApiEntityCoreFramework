using Serilog;

namespace LandingZoneWorkerService
{
    public class Worker : BackgroundService
    {

        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int a = 10, b = 0;
           
            var position = new { Latitude = 25, Longitude = 134 };
            var elapsedMs = 34;
            while (!stoppingToken.IsCancellationRequested)
            {
                var iteration = 1;
                _logger.LogDebug($"Debug {iteration}");
                _logger.LogInformation($"Information {iteration}");
                _logger.LogWarning($"Warning {iteration}");
                _logger.LogError($"Error {iteration}");
                _logger.LogCritical($"Critical {iteration}");
                Log.Warning("Start Task 1 :" + DateTimeOffset.Now);
                try
                {
                    Log.Debug("Dividing {A} by {B}", a, b);
                    Console.WriteLine(a / b);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Something went wrong");
                }
                finally
                {
                    Log.CloseAndFlush();
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}