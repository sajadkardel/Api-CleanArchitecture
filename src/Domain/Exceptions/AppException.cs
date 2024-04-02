using System.Net;

namespace Domain.Exceptions;

public class AppException(HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError, string message = null, Exception exception = null, object additionalData = null)
    : Exception(message, exception)
{
    public HttpStatusCode HttpStatusCode { get; set; } = httpStatusCode;
    public object AdditionalData { get; set; } = additionalData;
}
