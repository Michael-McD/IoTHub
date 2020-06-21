using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Opener
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;

        public Worker(ILogger<Worker> logger)
        {
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {            
            logger.LogInformation("Starting Garage Door Opener listener at: {time}", DateTimeOffset.Now);

            var msgProcessor = new MsgProcessor();
            msgProcessor.StartProcessorAsync();

           if (stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("CancellationToken received!");
                msgProcessor.StopProcessor();
            }
        }
    }
}
