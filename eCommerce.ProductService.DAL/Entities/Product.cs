namespace eCommerce.ProductService.DAL.Entities;

public class Product
{
    public Guid ProductID { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public double UnitOrice { get; set; }
    public int QuantityInStock { get; set; }
}