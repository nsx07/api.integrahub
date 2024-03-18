using IntegraHub.Domain.Dtos;
using IntegraHub.Domain.Interfaces;
using IntegraHub.Infra.CrossCutting.Integrations.Environment.Implementations;
using IntegraHub.Service.Utils.Extensions;
using RestSharp;
using System.Net.Mail;

namespace IntegraHub.Service.Services
{
    public class EnvironmentIntegrationService: IEnvironmentIntegrationService
    {
        private readonly GoDaddyProvider _goDaddyProvider;
        private readonly VercelProvider _vercelProvider;

        public EnvironmentIntegrationService() {
            this._vercelProvider = new VercelProvider();
            this._goDaddyProvider = new GoDaddyProvider();
        }

        ~EnvironmentIntegrationService()
        {
            this._vercelProvider.Dispose();
            this._goDaddyProvider.Dispose();
        }

        public async Task<ClientResultDto> AddNewClient(ClientDto clientDto)
        {
            try
            {
                _vercelProvider.Request.Resource = "v10/projects/agendahub/domains";
                _vercelProvider.Request.Method = Method.Post;
                _vercelProvider.Request.AddJsonBody(new
                {
                    name = clientDto.DomainName!.AppendIfNotPresent(".agendahub.app"),
                });


                var responseVercel = await _vercelProvider.Client.ExecuteAsync(_vercelProvider.Request);

                if (!responseVercel.IsSuccessful)
                {
                    return new ClientResultDto
                    {
                        Success = false,
                        Message = "Vercel error: " + responseVercel.ErrorMessage,
                        ProvidersMessages = new string[] { responseVercel.Content }
                    };
                }

                _goDaddyProvider.Request.Resource = "v1/domains/agendahub.app/records";
                _goDaddyProvider.Request.Method = Method.Patch;
                _goDaddyProvider.Request.AddJsonBody(new object[]
                {
                    new {
                        data = "cname.vercel-dns.com.",
                        name = clientDto.DomainName!.RemoveIfPresent(".agendahub.app"),
                        type = "CNAME",
                        port = 65535,
                        priority = 0,
                        weight = 0,
                        ttl = 600
                    }
                });

                var responseGoDaddy = await _goDaddyProvider.Client.ExecuteAsync(_goDaddyProvider.Request);

                if (!responseGoDaddy.IsSuccessful)
                {
                    return new ClientResultDto
                    {
                        Success = false,
                        Message = "GoDaddy error: " + responseGoDaddy.ErrorMessage,
                        ProvidersMessages = new string[] { responseGoDaddy.Content }
                    };
                }

                return new ClientResultDto
                {
                    Success = true,
                    Message = "Client successfully added!",
                    ProvidersMessages = new string[] { responseVercel.Content, responseGoDaddy.Content }
                };
            }
            catch (Exception e)
            {
                return new ClientResultDto
                {
                    Success = false,
                    Message = e.Message,
                    ProvidersMessages = new string[] { e.Message }
                };
            }
        }

        public async Task<ClientResultDto> RemoveClient(ClientDto clientDto)
        {
            try
            {
                _vercelProvider.Request.Resource = $"v9/projects/agendahub/domains/{clientDto.DomainName.AppendIfNotPresent(".agendahub.app")}";
                _vercelProvider.Request.Method = Method.Delete;

                _goDaddyProvider.Request.Resource = $"v1/domains/agendahub.app/records/CNAME/{clientDto.DomainName.RemoveIfPresent(".agendahub.app")}";
                _goDaddyProvider.Request.Method = Method.Delete;

                var responseVercel = await _vercelProvider.Client.ExecuteAsync(_vercelProvider.Request);

                if (!responseVercel.IsSuccessful)
                {
                    return new ClientResultDto
                    {
                        Success = false,
                        Message = "Vercel error: " + responseVercel.ErrorMessage,
                        ProvidersMessages = new string[] { responseVercel.Content }
                    };
                }

                var responseGoDaddy = await _goDaddyProvider.Client.ExecuteAsync(_goDaddyProvider.Request);

                if (!responseGoDaddy.IsSuccessful)
                {
                    return new ClientResultDto
                    {
                        Success = false,
                        Message = "GoDaddy error: " + responseGoDaddy.ErrorMessage,
                        ProvidersMessages = new string[] { responseGoDaddy.Content }
                    };
                }

                return new ClientResultDto
                {
                    Success = true,
                    Message = "Client successfully removed!",
                    ProvidersMessages = new string[] { responseVercel.Content, responseGoDaddy.Content }
                };
            }
            catch (Exception e)
            {
                return new ClientResultDto { 
                    Success = false,
                    Message = e.Message,
                    ProvidersMessages = new string[] { e.Message }
                };
            }
        }
    }
}
