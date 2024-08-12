using APIGateway.Application.CEP.UseCases.ConsultarCep;
using APIGateway.Application.CEP.UseCases.ConsultarCepAsync;
using APIGateway.Application.Presenters.CEP;
using APIGateway.Application.Profiles;
using APIGateway.Domain.CEP.Services;
using APIGateway.Infrastructure.CEPService.Services;
using APIGateway.Infrastructure.Data;
using APIGateway.Infrastructure.Data.Services;
using APIGateway.Infrastructure.Kafka;
using System.Reflection;

namespace APIGateway.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<PayloadService<ConsultarCepRequest, RequestDatabaseSettings>>();
        services.AddSingleton<PayloadService<CEPPresenter, ReponseDatabaseSettings>>();

        services.AddSingleton<PayloadService<ConsultarCepAsyncRequest, RequestDatabaseSettings>>();

        services.AddSingleton<IHostedService, Worker>();
        services.AddSingleton<ICEPService, CEPService>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load($"APIGateway.{nameof(Application)}")));
        services.AddAutoMapper(typeof(DTOMappingProfile));

        return services;
    }


    public static IServiceCollection UseKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ClientKafka>();

        return services;
    }

}