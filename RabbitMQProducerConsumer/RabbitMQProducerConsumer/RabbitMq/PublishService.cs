using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQProducerConsumer.RabbitMq;

public class PublishService
{
    public void Publish(EventDto eventDto)
    {
        ConnectionFactory factory = new();
        factory.HostName = "localhost";
        factory.Port = 5672;
        factory.UserName = "guest";
        factory.Password = "guest";  

        using IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();

        channel.QueueDeclare(queue: "queue-test", exclusive: false,durable:true);

        var message = JsonConvert.SerializeObject(eventDto);
        var body = Encoding.UTF8.GetBytes(message);
   
        channel.BasicPublish(exchange: "", routingKey: "queue-test", body: body);

           
    }
}