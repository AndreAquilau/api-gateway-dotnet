using APIGateway.Domain.CEP.ObjectValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Domain.Interfaces;
public interface ICEPRepository
{
    Task<CEPObjectValue> ConsultarCepAsync(string cep);
}
