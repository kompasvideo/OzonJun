using Microsoft.AspNetCore.Mvc;

namespace Route256_2_Demo.Controllers
{
    [ApiController]
    [Route("service")]
    public class ServiceController : Controller
    {
        // private readonly TransientService _one;
        // private readonly TransientService _two;
        //
        // public ServiceController(TransientService one, TransientService two)
        // {
        //     _one = one;
        //     _two = two;
        // }
        
        // private readonly ScopedService _one;
        // private readonly ScopedService _two;
        //
        // public ServiceController(ScopedService one, ScopedService two)
        // {
        //     _one = one;
        //     _two = two;
        // }
        
        private readonly SingltonService _one;
        private readonly SingltonService _two;

        public ServiceController(SingltonService one, SingltonService two)
        {
            _one = one;
            _two = two;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var isEqual = _one == _two;
            
            _one.AddOne();           
            _one.AddOne();
            _two.AddOne();

            return Json(new
            {
                IsEqual = isEqual,
                CountOne = _one.Count,
                CountTwo = _two.Count
            });
        }
    }
}