using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Service.Services.Auth.Dto
{
    public class AuthRequest
    {
        public string Login { get; set; }
        public string? Domain { get; set; }
        public string Password { get; set; }
    }
}
