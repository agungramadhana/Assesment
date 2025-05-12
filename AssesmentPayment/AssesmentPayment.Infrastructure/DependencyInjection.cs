using AssesmentPayment.Application;
using AssesmentPayment.Infrastructure.Authentications;
using AssesmentPayment.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AssesmentShared.Contract;
using MassTransit;

namespace AssesmentPayment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqSection = configuration.GetSection("RabbitMq");
            var config = rabbitMqSection.Get<RabbitMqSettingModel>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<EventPaymentConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(config.Host, config.VirtualHost, h =>
                    {
                        h.Username(config.UserName);
                        h.Password(config.Password);
                    });

                    cfg.Message<PublishEventMessageModel>(m =>
                    {
                        m.SetEntityName("event-create-payment");
                    });

                    cfg.ReceiveEndpoint("event-create-payment-queue", e =>
                    {
                        e.ConfigureConsumer<EventPaymentConsumer>(context);
                    });

                    cfg.Message<PaymentEventMessageModel>(m =>
                    {
                        m.SetEntityName("event-confirmation-payment");
                    });
                });
            });

            services.AddMassTransitHostedService();

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IPublishService, PublishService>();

            services.AddScoped<IJwtProvider, JwtProvider>();
            services.ConfigureOptions<JwtOptionSetup>();
            services.ConfigureOptions<JwtBearerTokenSetup>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
            });

            return services;
        }
    }
}
