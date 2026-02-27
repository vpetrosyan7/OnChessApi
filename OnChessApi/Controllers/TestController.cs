using Microsoft.AspNetCore.Mvc;

namespace OnChessApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger _logger;

        public TestController(ILogger<TestController> logger) 
        {
            _logger = logger;
        }

        [HttpGet]
        public IResult Get()
        {
            _logger.LogInformation("aaa");

            throw new Exception("some exception");

            return Results.Ok("Success Authorization");
        }
    }
}
