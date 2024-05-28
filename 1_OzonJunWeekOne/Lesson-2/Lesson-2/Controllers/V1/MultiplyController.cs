using Lessons_2.Bll.Models;
using Lessons_2.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lesson_2.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class MultiplyController : ControllerBase
{
    [HttpPost]
    public MultiplyResponse Multiply([FromServices] ICalculationService calculationService, 
        MultiplyRequest request)
    {
        var result = calculationService.Multiply(new OperandModel(
            request.Operand1,
            request.Operand2));
        return new MultiplyResponse(result);
    }
}