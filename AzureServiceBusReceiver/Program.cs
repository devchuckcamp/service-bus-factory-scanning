using Microsoft.Azure.ServiceBus;
using Common.Models;
using System.Text;
using System.Text.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AzureServiceBusReceiver;

class Program
{   

    const string connectionString = "[SendGrid_API_KEY]";
    const string queueName = "pickupqueue";
    static IQueueClient? queueClient;

    static async Task Main(string[] args)
    {
        Console.WriteLine("Picked Up Products:");
        queueClient = new QueueClient(connectionString, queueName);
        var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
        {
            MaxConcurrentCalls = 1,
            AutoComplete = false
        };

        queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

        Console.ReadLine();

        await queueClient.CloseAsync();
    }

    private static async Task ProcessMessagesAsync(Message message, CancellationToken token)
    {
        var jsonString = Encoding.UTF8.GetString(message?.Body);
  
        PickItem item = JsonSerializer.Deserialize<PickItem>(jsonString);
        Console.WriteLine($"Order: {item?.OrderId}/Item/s picked up: {item?.ItemSku}");
        if(item is not null)
        {
            await EmailNotifyCustomer(item);
        }

        await queueClient.CompleteAsync(message.SystemProperties.LockToken);
    }

    private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
    {
        Console.WriteLine($"Message exception: {arg.Exception}");
        return Task.CompletedTask;
    }

    private static async Task EmailNotifyCustomer(PickItem item)
    {
        try
        {
            //Your SendGrid API
            var client = new SendGridClient("");

            var from = new EmailAddress("[Sender_Email]", "[Sender_Name]");
            var to = new EmailAddress("[Receiver_Email]", "[Receiver_Name]");
            var subject = "Thank you!";
            var plainContent = $"Your order:{item.OrderId} with item/s: {item.ItemSku} has been picked up!";
            var htmlContent = $"Your order:<strong>{item.OrderId}</strong> with item/s: <strong>{item.ItemSku}</strong> has been picked up!";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainContent, htmlContent);

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Console.WriteLine("Email sent successfully.");
            }
            else
            {
                Console.WriteLine("Email failed to send.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
        }
    }

}