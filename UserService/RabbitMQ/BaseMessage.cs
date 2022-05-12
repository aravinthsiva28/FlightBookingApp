using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.RabbitMQ
{
    public class BaseMessage
    {
        public string Name { get; set; }
        public int value { get; set; }
    }
}
