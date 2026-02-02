using System.Text;
using System.Text.Json;
using eCommerce.ProductService.BLL.ServicesInterfaces;
using RabbitMQ.Client;

namespace eCommerce.ProductService.BLL.RabbitMQ;

public class RabbitMqPublisher(IConnection connection) : IMessagePublisher
{
    private readonly IConnection _connection = connection;

    public async Task PublishMessageAsync<T>(T message, string routingKey)
    {
        // 1. Create Channel
        await using var channel = await _connection.CreateChannelAsync();

        // 2. Declare Exchange
        await channel.ExchangeDeclareAsync(exchange: RabbitMqConstants.ProductsExchange, type: ExchangeType.Direct, durable: true);

        // 3. Serialize
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var props = new BasicProperties()
        {
            Persistent = true // Recommended: Saves message to disk if RabbitMQ restarts
        };


        // 4. Publish
        await channel.BasicPublishAsync(exchange: RabbitMqConstants.ProductsExchange,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: props,
            body: body);

        Console.WriteLine($" [x] Published to '{RabbitMqConstants.ProductsExchange}' with key '{routingKey}': {json}");
    }
}