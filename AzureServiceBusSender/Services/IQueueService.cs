namespace AzureServiceBusSender.Services;

public interface IQueueService
{
    Task SendMessageAsync<T>(T message, string queueName);
}
