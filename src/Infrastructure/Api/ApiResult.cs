using System.Net;
using System.Text.Json.Serialization;

namespace Infrastructure.Api;

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    public ApiResult(bool isSuccess, HttpStatusCode statusCode, string? message = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
    }
}

public class ApiResult<TData>(bool isSuccess, HttpStatusCode statusCode, TData data, string? message = null) 
    : ApiResult(isSuccess, statusCode, message) 
    where TData : class
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TData Data { get; set; } = data;
}
