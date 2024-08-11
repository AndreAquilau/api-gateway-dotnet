using Confluent.Kafka;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIGateway.Infrastructure.Kafka;
public class MessageCustom : Message<string, string>, IMessageCustom
{
    public MessageCustom(object payload, string transactionId)
    {
        Key = transactionId;
        var headers = new Headers();
        Headers = headers;



        Value = JsonSerializer.Serialize(new
        {
            transactionId,
            payload,
        });


    }

    public void AddHeader(string key, string value)
    {
        base.Headers.Add(new Header(key, UTF8Encoding.UTF8.GetBytes(value)));
    }
}
