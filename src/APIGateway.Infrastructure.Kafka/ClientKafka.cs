

using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace APIGateway.Infrastructure.Kafka;
public class ClientKafka 
{
   private readonly ClientConfig _clientConfig;
    public ClientKafka(ClientConfig clientConfig) {
        _clientConfig = clientConfig;
    }

    public async Task<string> BasicProducer(string payload)
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
                var message = new Message<string, string> { Key = uuid.ToString(), Value = payload};


                var dr = await p.ProduceAsync("cep-topic", message);
                Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");

                return uuid.ToString();

            }
            catch (ProduceException<string, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                throw new Exception(nameof(BasicProducer));
            }
        }
    }

    public IConsumer<Ignore, string> BasicConsumer(string group, string topic )
    {
        var conf = new ConsumerConfig(_clientConfig)
        {
            GroupId = group,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            AllowAutoCreateTopics = true
        };

        var c = new ConsumerBuilder<Ignore, string>(conf).Build();

        c.Subscribe(topic);

        return c;
    }

}