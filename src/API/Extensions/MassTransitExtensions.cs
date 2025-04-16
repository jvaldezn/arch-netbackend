using Data.Messaging.Consumer;
using Data.Util;
using MassTransit;

namespace API.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var mbHost = configuration.GetValue<string>(Constant.MBHost) ?? throw new InvalidOperationException();
            var mbUsername = configuration.GetValue<string>(Constant.MBUsername) ?? throw new InvalidOperationException();
            var mbPassword = configuration.GetValue<string>(Constant.MBPassword) ?? throw new InvalidOperationException();
            var mbRetryCount = Math.Max(configuration.GetValue<int>(Constant.MBRetryCount), 1);

            services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = false;
                options.StartTimeout = TimeSpan.FromSeconds(10);
                options.StopTimeout = TimeSpan.FromSeconds(20);
                options.ConsumerStopTimeout = TimeSpan.FromSeconds(20);
            });

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                busConfigurator.AddConsumer<LogCreatedConsumer>();

                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(mbHost), h =>
                    {
                        h.Username(mbUsername);
                        h.Password(mbPassword);
                    });

                    configurator.ConfigureEndpoints(context);

                    configurator.UseMessageRetry(r => r.Immediate(mbRetryCount));
                });
            });

            return services;
        }
    }
}
