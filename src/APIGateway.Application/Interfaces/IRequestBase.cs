using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Application.Interfaces;
public interface IRequestBase<TResponse> : IRequest<TResponse>
{
    string TransactionId { get; set; }
}
