using IntegraHub.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Domain.Interfaces
{
    public interface IEnvironmentIntegrationService
    {
        Task<ClientResultDto> AddNewClient(ClientDto clientDto);
    }
}
