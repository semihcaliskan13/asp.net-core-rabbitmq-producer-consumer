using Microsoft.AspNetCore.Mvc;
using RabbitMQProducerConsumer.RabbitMq;

namespace RabbitMQProducerConsumer.Controllers;

[ApiController]
[Route("produce")]
public class PublishController : ControllerBase
{
    private readonly PublishService _publishService;

    public PublishController(PublishService publishService)
    {
        _publishService = publishService;
    }

    [HttpGet]
    public void Get()
    {
        Random rnd = new Random();
        _publishService.Publish(new EventDto()
        {
            Id = rnd.Next(0,100)
        });
        
    }
    

}