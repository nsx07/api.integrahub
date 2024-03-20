using IntegraHub.Domain.Dtos;
using IntegraHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Domain.Interfaces
{
    public interface ICompanyService: IBaseService<Company, long>
    {
        Company? GetByDomainName(string domain);
        Task<dynamic> RegisterCompany(ClientDto company);
        Task<dynamic> UnregisterCompany(long id);
    }
}
