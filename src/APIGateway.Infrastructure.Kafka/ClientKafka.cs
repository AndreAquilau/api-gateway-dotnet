

using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace APIGateway.Infrastructure.Kafka;
public class ClientKafka
{
    private readonly string BootstrapServers = "localhost:9092";
    private readonly ClientConfig _clientConfig;
    private readonly IEnumerable<TopicSpecification> topics = new List<TopicSpecification>()
    {
     new TopicSpecification { Name = "cep-topic.request", ReplicationFactor = 1, NumPartitions = 1 },
     new TopicSpecification { Name = "cep-topic.response", ReplicationFactor = 1, NumPartitions = 1 },
    };

    public ClientKafka()
    {
        var clientConfig = new ClientConfig()
        {
            BootstrapServers = BootstrapServers,
            AllowAutoCreateTopics = true,
        };

        _clientConfig = clientConfig;

        OnCreate().Wait();
    }

    private async Task OnCreate()
    {

        //Criar Tópicos
        using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = BootstrapServers }).Build())
        {
            try
            {
                await adminClient.CreateTopicsAsync(topics);
            }
            catch (CreateTopicsException e)
            {
                Console.WriteLine($"Erro ao criar topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
            }
        }

    }

    public async Task<string> BasicProducer(string topic, string payload)
    {
        var uuid = Guid.NewGuid();
        var config = new ProducerConfig(_clientConfig);

        // If serializers are not specified, default serializers from
        // `Confluent.Kafka.Serializers` will be automatically used where
        // available. Note: by default strings are encoded as UTF8.
        using (var p = new ProducerBuilder<string, string>(config).Build())
        {
            try
            {
                var message = new Message<string, string> { Key = uuid.ToString(), Value = payload };


                var dr = await p.ProduceAsync(topic, message);
                Console.WriteLine($"Entregue '{dr.Value}' para '{dr.TopicPartitionOffset}'");

                return uuid.ToString();

            }
            catch (ProduceException<string, string> e)
            {
                Console.WriteLine($"Falha na entrega: {e.Error.Reason}");
                throw new Exception(nameof(BasicProducer));
            }
        }
    }

    public IConsumer<Ignore, string> BasicConsumer(string group, string topic)
    {
        var conf = new ConsumerConfig(_clientConfig)
        {
            GroupId = group,
            AllowAutoCreateTopics = true
        };

        var c = new ConsumerBuilder<Ignore, string>(conf).Build();

        c.Subscribe(topic);

        return c;
    }

}