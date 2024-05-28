using Lessons_2.Bll.Models;
using Lessons_2.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lesson_2.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class DivideController : ControllerBase
{
    [HttpPost]
    public DivideResponse Divide([FromServices] ICalculationService calculationService,
        DivideRequest request)
    {
        var result = calculationService.Divide(new OperandModel(
            request.Operand1,
            request.Operand2));
        return new DivideResponse(result);
    }
}