using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Questrade.FinCrime.Email.Intelligence.Domain.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class LexisNexisException : Exception
{
    public LexisNexisException() { }

    public LexisNexisException(string message) : base(message) { }

    public LexisNexisException(string message, Exception innerException) : base(message, innerException) { }

    protected LexisNexisException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

}