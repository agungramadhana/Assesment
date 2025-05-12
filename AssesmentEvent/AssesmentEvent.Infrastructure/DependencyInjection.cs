using AssesmentEvent.Application;
using AssesmentEvent.Infrastructure.Authentications;
using AssesmentEvent.Infrastructure;
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
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using AssesmentShared.Contract;
using MassTransit;

namespace AssesmentEvent.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqSection = configuration.GetSection("RabbitMq");
            var config = rabbitMqSection.Get<RabbitMqSettingModel>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<PaymentEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(config.Host, config.VirtualHost, h =>
                    {
                        h.Username(config.UserName);
                        h.Password(config.Password);
                    });

                    cfg.Message<PaymentEventMessageModel>(m =>
                    {
                        m.SetEntityName("event-confirmation-payment");
                    });

                    cfg.ReceiveEndpoint("event-confirmation-payment-queue", e =>
                    {
                        e.ConfigureConsumer<PaymentEventConsumer>(context);
                    });

                    cfg.Message<PublishEventMessageModel>(m =>
                    {
                        m.SetEntityName("event-create-payment");
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
