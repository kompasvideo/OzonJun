using Lessons_2.Bll.Models;
using Lessons_2.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lesson_2.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class SubtractController : ControllerBase
{
    [HttpPost]
    public SubtractResponse Substract([FromServices] ICalculationService calculationService, 
        SubtractRequest request)
    {
        var result = calculationService.Subtract(new OperandModel(
            request.Operand1,
            request.Operand2));
        return new SubtractResponse(result);
    }
}