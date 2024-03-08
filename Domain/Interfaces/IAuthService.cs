using IntegraHub.Service.Services.Auth.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest authRequest, bool? isMobile);
        Task<AuthResponse> Refresh();

    }
}
