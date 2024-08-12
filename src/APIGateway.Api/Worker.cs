using APIGateway.Application.CEP.UseCases.ConsultarCepAsync;
using APIGateway.Application.Presenters.CEP;
using APIGateway.Infrastructure.Kafka;
using MediatR;
using System.Text.Json;

namespace APIGateway.Api;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ClientKafka _clientKafka;
    private readonly IMediator _mediator;

    public Worker(ILogger<Worker> logger, ClientKafka clientKafka, IMediator mediator)
    {
        _logger = logger;
        _clientKafka = clientKafka;
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        var c = _clientKafka.BasicConsumer($"cep-consumer-group", "cep-topic.request");

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker Consumer - running at: {time}", DateTimeOffset.Now);
            }

            await Task.Run(async () =>
            {
                var cr = c.Consume(stoppingToken);

                var obj = JsonSerializer.Deserialize<MessageDataCustom>(cr.Message.Value);

                if (obj is not null)
                {
                    Console.WriteLine($"Mensagem consumida '{cr.Message.Value}' no: '{cr.TopicPartitionOffset}'.");

                    Console.WriteLine($"Decodificando mensagem '{cr.Message.Value}' no: '{cr.TopicPartitionOffset}'.");

                    var payload = JsonSerializer.Deserialize<ConsultarCepAsyncConsumerRequest>(obj.payload ?? "");

                    if (payload is not null)
                    {
                        Console.WriteLine($"Call Handler ConsultarCepAsyncConsumerRequest '{cr.Message.Value}' no: '{cr.TopicPartitionOffset}'.");

                        ConsultarCepAsyncConsumerRequest input = new ConsultarCepAsyncConsumerRequest(payload.Cep, payload.TransactionId);

                        await _mediator.Send(input);
                    }
                }

            });

            //await Task.Delay(1000, stoppingToken);
        }

        c.Dispose();
    }
}
