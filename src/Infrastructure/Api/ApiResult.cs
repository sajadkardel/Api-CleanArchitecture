using Domain.Utilities;
using Newtonsoft.Json;

namespace Infrastructure.Api;

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public ApiResultStatusCode StatusCode { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; set; }

    public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, string message = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message ?? statusCode.ToDisplay();
    }
}

public class ApiResult<TData> : ApiResult
    where TData : class
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public TData Data { get; set; }

    public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, TData data, string message = null)
        : base(isSuccess, statusCode, message)
    {
        Data = data;
    }
}
