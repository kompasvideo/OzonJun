using Lessons_2.Dal.Models;

namespace Lessons_2.Dal.Repositories.Interfaces;

public interface ICalculationHistoryRepository
{
    void Add(CalculationHistoryEntity entity);
    CalculationHistoryEntity[] Query();
}