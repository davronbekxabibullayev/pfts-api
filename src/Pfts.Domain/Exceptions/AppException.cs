namespace Pfts.Domain.Exceptions;

using System.Globalization;
using System.Runtime.Serialization;

public class AppException : Exception
{
    public string MessageFormat { get; } = string.Empty;

    public object[] Args { get; } = [];

    protected AppException()
    {
    }

    public AppException(string? message)
        : base(message)
    {
    }

    public AppException(string messageFormat, params object[] args)
        : base(string.Format(CultureInfo.InvariantCulture, messageFormat, args))
    {
        MessageFormat = messageFormat;
        Args = args;
    }

    public AppException(Exception? innerException, string messageFormat, params object[] args)
        : base(string.Format(CultureInfo.InvariantCulture, messageFormat, args), innerException)
    {
        MessageFormat = messageFormat;
        Args = args;
    }

    public AppException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected AppException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
