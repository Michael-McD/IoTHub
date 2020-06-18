using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Opener
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
            Console.WriteLine("Starting Garage Door Opener listener.");

            var msgProcessor = new MsgProcessor();
            msgProcessor.StartProcessor();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }

            if (stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("CancellationToken received!");
                msgProcessor.StopProcessor();
            }
        }
    }
}
