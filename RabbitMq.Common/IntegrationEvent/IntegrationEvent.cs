using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Common.IntegrationEvent
{

    public class IntegrationEvent
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
