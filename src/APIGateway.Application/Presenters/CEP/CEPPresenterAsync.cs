using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Application.Presenters.CEP;
public class CEPPresenterAsync
{
    public string?  TransactionId { get; set; }
    public string Topic { get; set; } = String.Empty;
    public int Offset { get; set; }
    public DateTime CreatedAtUtc { get; set; }

}
