using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Service.Services.Auth.Dto
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        
        public static AuthResponse Succeeded(string token) => new()
        {
            Success = true,
            Token = token
        };

        public static AuthResponse Succeeded(string token, string refreshToken) => new()
        {
            Token = token,
            Success = true,
            RefreshToken = refreshToken,
        };

        public static AuthResponse Failed(string message) => new()
        {
            Success = false,
            Message = message
        };
    }

}
