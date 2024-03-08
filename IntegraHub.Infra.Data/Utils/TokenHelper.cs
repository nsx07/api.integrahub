using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IntegraHub.Infra.CrossCutting.Utils
{
    public class TokenHelper
    {
        public static void ConfigureAuthToken(IConfiguration configuration, IServiceCollection services) 
        {
            services.AddAuthorization();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                // Adding Jwt Bearer  
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
                    };
                });
        }

        public static string GenerateToken(IConfiguration configuration, IEnumerable<Claim> claims, DateTime? expires = null)
        {
            ClaimsIdentity claimsIdentity = new(claims);
            SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));
            SigningCredentials signingCredentials = new(authSigningKey, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = claimsIdentity,
                Issuer = configuration["JWT:ValidIssuer"],
                Audience = configuration["JWT:ValidAudience"],
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = signingCredentials,
                Expires = expires ?? DateTime.UtcNow.AddHours(3)
            };

            JwtSecurityTokenHandler tokenHandler = new();

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
