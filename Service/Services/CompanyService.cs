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

                if (integration == null || !integration.Success)
                    return integration;
            }
            catch (Exception ex)
            {
                throw ex;
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
                Id = 0
            };

            return (await Add<CompanyValidator>(company)).Id;
        }


    }
}
