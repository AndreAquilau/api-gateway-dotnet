using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Infrastructure.CEPService.CepException;
public class CepException : Exception
{
    public string erro { get; set; } = string.Empty;
    public CepException(string message) : base(message: message) { }
}
