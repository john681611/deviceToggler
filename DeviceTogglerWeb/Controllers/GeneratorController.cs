using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeviceToggler;

namespace DeviceToggler.GUI.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneratorController : ControllerBase
    {
        private readonly ILogger<GeneratorController> _logger;

        public GeneratorController(ILogger<GeneratorController> logger)
        {
            _logger = logger;
        }
    }
}
