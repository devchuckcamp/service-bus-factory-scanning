using AzureServiceBusSender.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace AzureServiceBusSender.Repository;

public class QueueService:IQueueService
{   
    private readonly IConfiguration _config;
    public QueueService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendMessageAsync<T>(T serviceBusMessage, string queueName)
    {
        var queueClient = new QueueClient(_config.GetConnectionString("AzureServiceBus"), queueName);
        string messageBody = JsonSerializer.Serialize(serviceBusMessage);
        var message = new Message(Encoding.UTF8.GetBytes(messageBody));

        await queueClient.SendAsync(message);
    }
}
