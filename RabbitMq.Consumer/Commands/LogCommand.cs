using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMq.Consumer.Commands
{
    public class LogCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
