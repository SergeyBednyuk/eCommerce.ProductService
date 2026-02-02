namespace eCommerce.ProductService.BLL.RabbitMQ;

public static class RabbitMqConstants
{
    // Exchange Names
    public const string ProductsExchange = "products.exchange";

    // Routing Keys
    public static class RoutingKeys
    {
        public const string ProductCreated = "product.created";
        public const string ProductUpdated = "product.updated";
        public const string ProductDeleted = "product.deleted";
        public const string ProductStockUpdated = "product.stockupdated";
    }
}