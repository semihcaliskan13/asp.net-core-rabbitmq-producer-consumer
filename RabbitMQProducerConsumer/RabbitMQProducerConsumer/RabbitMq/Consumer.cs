
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQProducerConsumer.RabbitMq;

public class Consumer
{
    public void Register()
    {
        ConnectionFactory factory = new();
        factory.HostName = "localhost";
        factory.Port = 5672;
        factory.UserName = "guest";
        factory.Password = "guest";

         IConnection connection = factory.CreateConnection();
         IModel channel = connection.CreateModel();
         
        EventingBasicConsumer consumer = new(channel);
        channel.BasicConsume("queue-test", autoAck: false, consumer);
        consumer.Received += (sender, e) =>
        {
            string jsonData = Encoding.UTF8.GetString(e.Body.Span);
            EventDto eventDto = JsonConvert.DeserializeObject<EventDto>(jsonData);
            Console.WriteLine(eventDto.Id);
            channel.BasicAck(deliveryTag: e.DeliveryTag, false);
        };
    }
}

public class EventDto
{
    public int Id { get; set; }
}