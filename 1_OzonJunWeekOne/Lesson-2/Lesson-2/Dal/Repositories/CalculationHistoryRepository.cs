using Lessons_2.Dal.Models;
using Lessons_2.Dal.Repositories.Interfaces;

namespace Lessons_2.Dal.Repositories;

public class CalculationHistoryRepository : ICalculationHistoryRepository
{
    private List<CalculationHistoryEntity> _storage = new();

    public void Add(CalculationHistoryEntity entity)
    {
        _storage.Add(entity);
    }

    public CalculationHistoryEntity[] Query()
    {
        return _storage.ToArray();
    }
}