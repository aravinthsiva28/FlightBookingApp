using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.RabbitMQ
{
    public class MessageSender : IMessageSender
    {
        
        public void SendMessage(BaseMessage msg, string queueName)
        {
            var factory = new ConnectionFactory
            {

                Uri = new Uri("amqp://guest:guest@127.0.0.1:5672")

            };
            
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var messgae = new { Name = "producer", Messgae = "hello" };

            var body  = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messgae));

            channel.BasicPublish("", queueName, null, body);

        }

    }
}
