using QRCodeGeneratorApp.Api.Healthcheck.Checks;

namespace QRCodeGeneratorApp.Api.BackgroundTasks
{
    public class StartupBackgroundService : BackgroundService
    {
        //Inject the startuphealthcheck endpoint to signal when the startup task is completed
        //Can be implemented better with an interface
        private readonly StartupHealthCheck startupHealthCheck;

        public StartupBackgroundService(StartupHealthCheck startupHealthCheck)
        {
            this.startupHealthCheck = startupHealthCheck;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Simulate long-running startup task
            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);

            //Signal startup completion
            startupHealthCheck.SignaStartupCompleted();
        }
    }
}
