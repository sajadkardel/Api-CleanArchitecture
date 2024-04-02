using System.Net;

namespace Domain.Exceptions;

public class NotFoundException(string message = null, Exception exception = null, object additionalData = null)
    : AppException(HttpStatusCode.NotFound, message, exception, additionalData)
{
}
