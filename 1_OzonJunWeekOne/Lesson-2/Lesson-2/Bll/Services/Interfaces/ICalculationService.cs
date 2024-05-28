using Lessons_2.Bll.Models;

namespace Lessons_2.Bll.Services.Interfaces;

public interface ICalculationService
{
    double Add(OperandModel data);
    double Subtract(OperandModel data);
    double Divide(OperandModel data);
    double Multiply(OperandModel data);
    string[] QueryHistory();
}