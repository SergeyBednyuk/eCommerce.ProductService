namespace eCommerce.ProductService.BLL.ServicesInterfaces;

public interface IMessagePublisher
{
    Task PublishMessageAsync<T>(T message, string routingKey);
}