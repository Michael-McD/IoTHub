using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Opener.Models;

namespace Opener
{
    public static class ServiceProviderBuilder
    {
        public static IServiceProvider GetServiceProvider(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets(typeof(Program).Assembly)
                .Build();
            var services = new ServiceCollection();

            services.Configure<SecretOptions>(configuration.GetSection(typeof(SecretOptions).FullName));

            var provider = services.BuildServiceProvider();
            return provider;
        }
    }
}
