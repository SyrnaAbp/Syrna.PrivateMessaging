﻿using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;

namespace Syrna.PrivateMessaging
{
    public class ConsoleTestAppHostedService : IHostedService
    {
        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = await AbpApplicationFactory.CreateAsync<PrivateMessagingConsoleApiClientModule>())
            {
                await application.InitializeAsync();

                var demo = application.ServiceProvider.GetRequiredService<ClientDemoService>();
                await demo.RunAsync();

                await application.ShutdownAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
