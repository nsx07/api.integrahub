using IntegraHub.Domain.Dtos;
using IntegraHub.Domain.Entities;
using IntegraHub.Domain.Interfaces;
using IntegraHub.Service.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegraHub.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientDto company)
        {
            if (company == null)
                return NotFound();

            return await ExecuteAsync(async () => await _companyService.RegisterCompany(company));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Company company)
        {
            if (company == null)
                return NotFound();

            return await ExecuteAsync(async () => await _companyService.Update<CompanyValidator>(company));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id == 0)
                return NotFound();

            await ExecuteAsync(async () =>
            {
                await _companyService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await ExecuteAsync(async () => await _companyService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            if (id == 0)
                return NotFound();

            return await ExecuteAsync(async () => await _companyService.GetById(id));
        }
    }
}
