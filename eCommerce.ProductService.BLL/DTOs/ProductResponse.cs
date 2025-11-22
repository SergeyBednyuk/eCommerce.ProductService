namespace eCommerce.ProductService.BLL.DTOs;

public class ProductResponse<T> where T : class 
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    private ProductResponse(bool isSuccess, T? data, string? message, IEnumerable<string>? errors)
    {
        IsSuccess = isSuccess;
        Data = data;
        Message = message;
        Errors = errors;
    }

    public static ProductResponse<T> Success(T data, string? message = null)
    {
        return new ProductResponse<T>(true, data, message, null);
    }
    
    public static ProductResponse<T> Failure(string? message = null, IEnumerable<string>? errors = null)
    {
        return new ProductResponse<T>(false, null, message, errors);
    }
}