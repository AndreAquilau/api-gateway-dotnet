using APIGateway.Domain.CEP.ObjectValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Domain.CEP.Services;
public interface ICEPService
{
    Task<CEPObjectValue> ConsultarCepAsync(string cep);
}
