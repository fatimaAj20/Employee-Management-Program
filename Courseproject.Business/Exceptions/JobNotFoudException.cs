using System.Runtime.Serialization;

namespace Courseproject.Business.Exceptions;

[Serializable]
public class JobNotFoudException : Exception
{
    public int Id { get; }

    public JobNotFoudException()
    {
    }

    public JobNotFoudException(int id)
    {
        this.Id = id;
    }

    public JobNotFoudException(string? message) : base(message)
    {
    }

    public JobNotFoudException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected JobNotFoudException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}