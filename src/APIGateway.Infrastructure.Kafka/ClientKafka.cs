

using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace APIGateway.Infrastructure.Kafka;

public class ClientKafka
{

    private string BROKER_URL { get; set; } = "localhost:9092";
    public async Task<string> BasicProducer(string payload)
    {
        var uuid = Guid.NewGuid();
        var config = new ProducerConfig { BootstrapServers = BROKER_URL };

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

    public IConsumer<Ignore, string> BasicConsumer(string group, string topic)
    {
        var conf = new ConsumerConfig
        {
            GroupId = group,
            BootstrapServers = BROKER_URL,
            // Note: The AutoOffsetReset property determines the start offset in the event
            // there are not yet any committed offsets for the consumer group for the
            // topic/partitions of interest. By default, offsets are committed
            // automatically, so in this example, consumption will only start from the
            // earliest message in the topic 'my-topic' the first time you run the program.
            AutoOffsetReset = AutoOffsetReset.Earliest,
            AllowAutoCreateTopics = true
        };

        var c = new ConsumerBuilder<Ignore, string>(conf).Build();

        c.Subscribe(topic);

        return c;
    }

}