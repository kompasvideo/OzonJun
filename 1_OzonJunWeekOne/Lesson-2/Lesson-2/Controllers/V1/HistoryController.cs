using Lessons_2.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lesson_2.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class HistoryController : ControllerBase
{
    [HttpGet]
    public HistoryResponse Get([FromServices] ICalculationService calculationService)
    {
        var result = calculationService.QueryHistory();
        return new HistoryResponse(result);
    }
}