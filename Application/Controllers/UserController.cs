using IntegraHub.Domain.Entities;
using IntegraHub.Domain.Interfaces;
using IntegraHub.Infra.CrossCutting.Integrations.Environment.Implementations;
using IntegraHub.Service.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IntegraHub.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : BaseController
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (user == null)
                return NotFound();

            return await ExecuteAsync(async () => (await _userService.Add<UserValidator>(user)).Id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            if (user == null)
                return NotFound();

            return await ExecuteAsync(async () => await _userService.Update<UserValidator>(user));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id == 0)
                return NotFound();

            await ExecuteAsync(async () =>
            {
                await _userService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await ExecuteAsync(async () => await _userService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            if (id == 0)
                return NotFound();

            return await ExecuteAsync(async () => await _userService.GetById(id));
        }

    }
}
