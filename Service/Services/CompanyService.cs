using IntegraHub.Domain.Dtos;
using IntegraHub.Domain.Entities;
using IntegraHub.Domain.Interfaces;
using IntegraHub.Service.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Service.Services
{
    public class CompanyService(ICompanyRepository baseRepository, IEnvironmentIntegrationService _environmentIntegrationService) : BaseService<Company, long>(baseRepository), ICompanyService
    {
        public Company? GetByDomainName(string domain)
        {
            return _baseRepository.Query().Where(x => x.DomainName == domain).FirstOrDefault();
        }

        public async Task<dynamic> RegisterCompany(ClientDto client)
        {

            try
            {
                var integration = await _environmentIntegrationService.AddNewClient(client);

                if (!integration.Success)
                    return integration;
            }
            catch (Exception ex)
            {
                return ex;
            }

            Company company = new()
            {
                CreatedAt = DateTime.Now.ToUniversalTime(),
                UpdatedAt = DateTime.Now.ToUniversalTime(),
                FullName = client.FullName,
                DomainName = client.DomainName,
                LogoUrl = client.LogoUrl,
                IsActive = client.IsActive,
                Email = client.Email,
                Phone = client.Phone,
                Address = client.Address,
                City = client.City,
                State = client.State,
                ZipCode = client.ZipCode,
                Number = client.Number,
                Id = 0,
            };

            Company companySaved;

            try
            {
                companySaved = await Add<CompanyValidator>(company);
            }
            catch (Exception ex)
            {
                await _environmentIntegrationService.RemoveClient(client);
                return ex;
            }

            return companySaved.Id;
        }

        public async Task<dynamic> UnregisterCompany(long id)
        {
            var company = await GetById(id);

            if (company == null)
                return false;

            try
            {
                await _environmentIntegrationService.RemoveClient(new() { DomainName = company.DomainName });
            }
            catch (Exception ex)
            {
                return false;
            }

            company.IsActive = false;
            company.UpdatedAt = DateTime.Now.ToUniversalTime();
            company.CreatedAt = company.CreatedAt.HasValue ? company.CreatedAt.Value.ToUniversalTime() : DateTime.Now.ToUniversalTime();
            await Update<CompanyValidator>(company);

            return true;
        }

    }
}
