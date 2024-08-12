using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Infrastructure.Kafka;
public class MessageDataCustom
{
    public string? transactionId { get; set; }
    public dynamic? payload { get; set; }
}
