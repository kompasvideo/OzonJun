using Lessons_2.Bll.Models;
using Lessons_2.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lesson_2.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class AddController : ControllerBase
{
    [HttpPost]
    public AddResponse Add([FromServices] ICalculationService calculationService,
        AddRequest request)
    {
        var result = calculationService.Add(new OperandModel(
            request.Operand1,
            request.Operand2));
        return new AddResponse(result);
    }
}