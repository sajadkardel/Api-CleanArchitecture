using Domain.Utilities;
using System.Text.Json.Serialization;

namespace Infrastructure.Api;

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public ApiResultStatusCode StatusCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Message { get; set; }

    public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, string? message = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message ?? statusCode.ToDisplay();
    }
}

public class ApiResult<TData>(bool isSuccess, ApiResultStatusCode statusCode, TData data, string? message = null) 
    : ApiResult(isSuccess, statusCode, message) 
    where TData : class
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TData Data { get; set; } = data;
}
