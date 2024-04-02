using System.Net;

namespace Domain.Exceptions;

public class BadRequestException(string message = null, Exception exception = null, object additionalData = null)
    : AppException(HttpStatusCode.BadRequest, message, exception, additionalData)
{
}
