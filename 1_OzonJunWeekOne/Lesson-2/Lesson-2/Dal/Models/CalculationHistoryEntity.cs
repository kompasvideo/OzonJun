using Lessons_2.Dal.Enums;

namespace Lessons_2.Dal.Models;

public record CalculationHistoryEntity
(
  double Operand1,  
  double Operand2,
  OperationTypes Operation,
  double Result,
  DateTime At
);