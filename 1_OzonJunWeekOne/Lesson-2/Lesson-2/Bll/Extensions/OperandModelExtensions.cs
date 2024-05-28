using Lessons_2.Bll.Enums;
using Lessons_2.Bll.Exceptions;
using Lessons_2.Bll.Models;

namespace Lessons_2.Bll.Extensions;

public static class OperandModelExtensions
{
    private const double maxValue = Double.MaxValue / 100; 
    private const double minValue = Double.MinValue / 100;

    public static OperandModel EnsureNotNull(this OperandModel? src)
    {
        ArgumentNullException.ThrowIfNull(src);
        return src;
    }
    
    public static OperandModel EnsureNotOverflows(this OperandModel? src)
    {
        if (src.Operand1 >= maxValue
            && src.Operand2 >= maxValue)
        {
            throw new ArithmeticOverflowException(OverflowReason.ResultsToBig);
        }
        if (src.Operand1 <= maxValue
            && src.Operand2 <= maxValue)
        {
            throw new ArithmeticOverflowException(OverflowReason.ResultsToSmall);
        }
        return src;
    }
    
    public static OperandModel EnsureDivisionAllowed(this OperandModel src)
    {
        if (src.Operand2 == 0)
        {
            throw new DivisionByZeroException();
        }
        return src;
    }
}