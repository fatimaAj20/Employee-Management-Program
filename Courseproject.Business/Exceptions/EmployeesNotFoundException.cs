using System.Runtime.Serialization;

namespace Courseproject.Business.Exceptions;

[Serializable]
public class EmployeesNotFoundException : Exception
{
    public int[] EmployeesIds { get; }

    public EmployeesNotFoundException()
    {
    }

    public EmployeesNotFoundException(int[] employeesIds)
    {
        EmployeesIds = employeesIds;
    }

    public EmployeesNotFoundException(string? message) : base(message)
    {
    }

    public EmployeesNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected EmployeesNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}