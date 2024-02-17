using IntegraHub.Domain.Dtos;
using IntegraHub.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace IntegraHub.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentIntegrationController(IEnvironmentIntegrationService environmentIntegrationService) : BaseController
    {
        private readonly IEnvironmentIntegrationService _environmentIntegrationService = environmentIntegrationService;

        [HttpPost("AddNewClient")]
        public async Task<IActionResult> Post([FromBody] ClientDto clientDto)
        {
            return await ExecuteAsync(async () => await _environmentIntegrationService.AddNewClient(clientDto));
        }

        [HttpDelete("RemoveClient")]
        public async Task<IActionResult> Cancel([FromQuery] ClientDto clientDto)
        {
            return await ExecuteAsync(async () => await _environmentIntegrationService.RemoveClient(clientDto));
        }

    }
}
