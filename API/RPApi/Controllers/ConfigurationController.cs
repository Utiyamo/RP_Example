using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPApi.Models;
using RPApi.MongoDB.DAO;
using System.Threading.Tasks;

namespace RPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly ConfigurationService _confService = new ConfigurationService();

        [HttpGet]
        public async Task<ActionResult<Configurations>> getConfigurations([FromQuery] string codEmpresa)
        {
            return _confService.getConfiguration(codEmpresa).Result;
        }

        [HttpPost]
        public async Task<ActionResult<Configurations>> CreateConfiguration([FromBody] Configurations model)
        {
            var result = _confService.CreateNewConfiguration(model).Result;
            return result;
        }

        [HttpPut]
        public async Task<ActionResult<Configurations>> updateConfiguration([FromBody] Configurations model)
        {
            return _confService.UpdateConfiguration(model).Result;
        }
    }
}
