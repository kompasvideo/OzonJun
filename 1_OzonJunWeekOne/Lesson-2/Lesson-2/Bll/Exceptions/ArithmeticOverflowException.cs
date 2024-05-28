using Lessons_2.Bll.Enums;

namespace Lessons_2.Bll.Exceptions;

public class ArithmeticOverflowException : ArithmeticException
{
    public ArithmeticOverflowException(OverflowReason reason)
        : base(GetMessageByReason(reason))
    {
    }

    private static string GetMessageByReason(OverflowReason reason)
        => reason switch
        {
            OverflowReason.ResultsToBig => "Результат слишком большое число",
            OverflowReason.ResultsToSmall => "Результат слишком маленькое число",
            _ => throw new ArgumentOutOfRangeException(nameof(reason), reason, "Ошибка")
        };
}