using IntegraHub.Domain.Entities;
using IntegraHub.Domain.Enums;
using IntegraHub.Domain.Interfaces;
using IntegraHub.Infra.CrossCutting.Utils;
using IntegraHub.Infra.Data.Repository;
using IntegraHub.Service.Services.Auth.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Service.Services.Auth
{
    public class AuthService : IAuthService
    {

        private readonly IUserService userService;
        private readonly IConfiguration configuration;
        private readonly ICompanyService companyService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly CurrentClientResolver currentClientResolver;

        public AuthService(IUserService userService, IConfiguration configuration, CurrentClientResolver currentClientResolver, IHttpContextAccessor httpContextAccessor, ICompanyService companyService)
        {
            this.userService = userService;
            this.configuration = configuration;
            this.companyService = companyService;
            this.httpContextAccessor = httpContextAccessor;
            this.currentClientResolver = currentClientResolver;
        }

        public async Task<AuthResponse> Login(AuthRequest authRequest, bool? isMobile)
        {

            AuthResponse authResponse = new()
            {
                Success = false
            };

            try
            {
                //var domainStatus = this.ValidDomain(authRequest.Domain);
                //if (domainStatus.Status == DomainStatus.Invalid)
                //{
                //    authResponse.Message = "Domínio inválido.";
                //    return authResponse;
                //}

                //var company = companyService.GetByDomainName(domainStatus.Status == DomainStatus.Valid
                //                        ? authRequest.Domain!
                //                        : configuration["devCompany"] ?? domainStatus.Name!);

                //if (company == null)
                //{
                //    authResponse.Message = "Domínio inválido.";
                //    return authResponse;
                //}

                //if (!company.IsActive)
                //{
                //    authResponse.Message = "Conta inativa. Entre em contato com o suporte!";
                //    return authResponse;
                //}

                //var token = TokenHelper.GenerateToken(configuration, new Claim[] { new("companyId", company.Id.ToString()) });
                //httpContextAccessor.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";
                //httpContextAccessor.HttpContext.Request.Headers[$"companyId{httpContextAccessor.HttpContext.Request.Headers.RequestId}"] = company.Id.ToString();

                if (!string.IsNullOrEmpty(authRequest.Login) && !string.IsNullOrEmpty(authRequest.Password))
                {
                    User? userByLogin = userService.GetUserByLogin(authRequest.Login);

                    if (userByLogin != null)
                    {
                        if (userByLogin.UserTypeId == (int)UserTypeEnum.Admin)
                        {
                            authResponse.Message = "Clientes ainda não podem acessar a plataforma";
                            return authResponse;
                        }

                        if (userService.CheckPassword(authRequest.Password, userByLogin))
                        {
                            authResponse.Success = true;
                            authResponse.Message = "Logado com sucesso.";
                            authResponse.Token = this.GenerateToken(userByLogin, isMobile);

                            return authResponse;
                        }
                        else
                        {
                            authResponse.Message = "Senha incorreta.";
                            return authResponse;
                        }
                    }

                    authResponse.Message = "Usuário não cadastrado.";

                    return authResponse;
                }

                authResponse.Message = "Usuário e senha não podem ser nulos.";
            }
            catch (Exception ex)
            {
                authResponse.Message = ex.Message;
            }

            return authResponse;
        }

        public string GenerateToken(User user, bool? isMobile)
        {
            ClaimsIdentity claimsIdentity = new(new Claim[]
                            {
                        new("name", user.Name),
                        new(ClaimTypes.GivenName, user.Name),
                        new(ClaimTypes.Role, user.UserType.Code),
                        new("companyId", user.Company.Id.ToString()),
                        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new(JwtRegisteredClaimNames.Iat, DateTime.Now.ToFileTimeUtc().ToString()),
                            });

            return TokenHelper.GenerateToken(configuration , claimsIdentity.Claims, isMobile.HasValue && isMobile.Value ? DateTime.UtcNow.AddDays(1) : null);
        }

        private Domain ValidDomain(string? domainBody)
        {
            StringValues originByHeader = currentClientResolver.GetHeader("Origin");
            StringValues userAgent = currentClientResolver.GetHeader("User-Agent");

            if (userAgent.Count > 0 && userAgent[0].Contains("Mozilla"))
            {
                string origin = originByHeader[0];
                if (origin.Contains("localhost"))
                {
                    return new Domain() { Name = domainBody ?? "dev", Status = DomainStatus.Localhost };
                }
                if (originByHeader.Count > 0 && (!string.IsNullOrEmpty(domainBody) && origin.Contains(domainBody)))
                {
                    return new Domain() { Name = domainBody, Status = DomainStatus.Valid };
                }
                else
                {
                    return new Domain() { Status = DomainStatus.Invalid };
                }

            }

            if (userAgent.Count > 0 && userAgent[0].Contains("okhttp") && !string.IsNullOrEmpty(domainBody))
            {
                return new Domain() { Name = domainBody, Status = DomainStatus.Valid };
            }
            return new Domain() { Status = DomainStatus.Invalid };

        }

        public async Task<AuthResponse> Refresh()
        {
            if (this.currentClientResolver != null)
            {
                var nameid = this.currentClientResolver.Get("nameid");
                long id = long.Parse(nameid ?? "0");

                if (id > 0)
                {
                    User? userByLogin = await userService.GetById(id);

                    if (userByLogin != null)
                    {
                        if (!userByLogin.IsActive)
                        {
                            return AuthResponse.Failed("Usuário desabilitado. Entre em contato com o suporte.");
                        }

                        return AuthResponse.Succeeded(this.GenerateToken(userByLogin, false));
                    }
                    return AuthResponse.Failed("Usuário não encontrado.");
                }
                return AuthResponse.Failed("Id do portador do token não encontrado.");
            }
            return AuthResponse.Failed("Token desabilitado.");
        }
    }

    record Domain
    {
        public string? Name { get; init; }
        public DomainStatus Status { get; init; }
    }

    enum DomainStatus
    {
        Invalid, Valid, Localhost
    }
}
