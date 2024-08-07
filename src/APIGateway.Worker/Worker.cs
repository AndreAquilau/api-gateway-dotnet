using APIGateway.Infrastructure.Kafka;
using Confluent.Kafka;

namespace APIGateway.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ClientKafka _clientKafka;

    public Worker(ILogger<Worker> logger, ClientKafka clientKafka)
    {
        _logger = logger;
        _clientKafka = clientKafka;
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

            await Task.Run(() =>
            {
                var cr = c.Consume(stoppingToken);
                Console.WriteLine($"Mensagem consumida '{cr.Message.Value}' no: '{cr.TopicPartitionOffset}'.");
            });

            await Task.Delay(1000, stoppingToken);
        }

        c.Dispose();
    }
}
