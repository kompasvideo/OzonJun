namespace Lessons_2.Bll.Exceptions;

public class DivisionByZeroException : DivideByZeroException
{
    public DivisionByZeroException()
    {
    }

    public DivisionByZeroException(string? message) : base(message)
    {
    }
}