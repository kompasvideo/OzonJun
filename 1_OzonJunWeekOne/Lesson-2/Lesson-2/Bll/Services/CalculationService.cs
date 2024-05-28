using Lessons_2.Bll.Extensions;
using Lessons_2.Bll.Models;
using Lessons_2.Bll.Services.Interfaces;
using Lessons_2.Dal.Enums;
using Lessons_2.Dal.Models;
using Lessons_2.Dal.Repositories.Interfaces;

namespace Lessons_2.Bll.Services;

public class CalculationService : ICalculationService
{
    private readonly ICalculationHistoryRepository _calculationHistoryRepository;

    public CalculationService(ICalculationHistoryRepository calculationHistoryRepository)
    {
        _calculationHistoryRepository = calculationHistoryRepository;
    }

    public double Add(OperandModel data)
    {
        data.EnsureNotNull()
            .EnsureNotOverflows();
        var result = data.Operand1 + data.Operand2;
        _calculationHistoryRepository.Add(new CalculationHistoryEntity(
            data.Operand1,
            data.Operand2,
            OperationTypes.Sum,
            result,
            DateTime.Now));
        return result;
    }
    public double Subtract(OperandModel data)
    {
        data.EnsureNotNull()
            .EnsureNotOverflows();
        var result = data.Operand1 - data.Operand2;
        _calculationHistoryRepository.Add(new CalculationHistoryEntity(
            data.Operand1,
            data.Operand2,
            OperationTypes.Subtraction,
            result,
            DateTime.Now));
        return result;
    }
    public double Divide(OperandModel data)
    {
        data.EnsureNotNull()
            .EnsureNotOverflows()
            .EnsureDivisionAllowed();
        var result = data.Operand1 / data.Operand2;
        _calculationHistoryRepository.Add(new CalculationHistoryEntity(
            data.Operand1,
            data.Operand2,
            OperationTypes.Division,
            result,
            DateTime.Now));
        return result;
    }
    public double Multiply(OperandModel data)
    {
        data.EnsureNotNull()
            .EnsureNotOverflows();
        var result = data.Operand1 * data.Operand2;
        _calculationHistoryRepository.Add(new CalculationHistoryEntity(
            data.Operand1,
            data.Operand2,
            OperationTypes.Multiplication,
            result,
            DateTime.Now));
        return result;
    }

    public string[] QueryHistory()
    {
        return _calculationHistoryRepository.Query()
            .OrderByDescending(x => x.At)
            .Select(x =>
            {
                var operationSign = x.Operation switch
                {
                    OperationTypes.Sum => '+',
                    OperationTypes.Subtraction => '-',
                    OperationTypes.Multiplication => '*',
                    OperationTypes.Division => '/',
                    _ => throw new ArgumentOutOfRangeException()
                };
                return $"{x.At:hh:mm:ss} {x.Operand1:F} {operationSign} {x.Operand2:F} = {x.Result}";
            })
            .ToArray();
    }
}