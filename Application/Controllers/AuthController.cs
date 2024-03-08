using IntegraHub.Domain.Entities;
using IntegraHub.Infra.CrossCutting.Utils;
using IntegraHub.Service.Services.Auth.Dto;
using IntegraHub.Service.Services.Auth;
using IntegraHub.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IntegraHub.Domain.Interfaces;

namespace IntegraHub.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly CurrentClientResolver currentClientResolver;
        public AuthController(CurrentClientResolver currentClientResolver, IAuthService authService)
        {
            this.authService = authService;
            this.currentClientResolver = currentClientResolver;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<AuthResponse> Login([FromBody] AuthRequest authRequest, [FromQuery] bool? isMobile)
        {

            return await this.authService.Login(authRequest, isMobile);
        }

        [HttpGet("Refresh")]
        [Authorize]
        public async Task<ActionResult> Refresh()
        {
            var refresh = await this.authService.Refresh();

            if (refresh.Success)
            {
                return Ok(refresh);
            }

            return StatusCode(StatusCodes.Status404NotFound, refresh.Message);
        }

        [HttpGet("Check")]
        [Authorize]
        public ActionResult Check()
        {
            return Ok();
        }

    }

}
