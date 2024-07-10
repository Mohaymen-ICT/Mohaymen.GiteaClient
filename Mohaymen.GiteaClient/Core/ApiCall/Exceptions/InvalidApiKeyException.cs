using System;

namespace Mohaymen.GiteaClient.Core.ApiCall.Exceptions;

[Serializable]
public class InvalidApiKeyException : Exception
{
    public InvalidApiKeyException ()
    {}

    public InvalidApiKeyException (string message) : base(message)
    {}

    public InvalidApiKeyException (string message, Exception innerException) : base (message, innerException)
    {}  
}