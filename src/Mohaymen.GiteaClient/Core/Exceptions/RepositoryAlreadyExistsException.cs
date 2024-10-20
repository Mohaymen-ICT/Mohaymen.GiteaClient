using System;

namespace Mohaymen.GiteaClient.Core.Exceptions;

public class RepositoryAlreadyExistsException : Exception
{
    public RepositoryAlreadyExistsException()
    {
    }

    public RepositoryAlreadyExistsException(string message) : base(message)
    {
    }

    public RepositoryAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}