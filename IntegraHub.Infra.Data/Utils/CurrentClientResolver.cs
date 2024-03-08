using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IntegraHub.Infra.CrossCutting.Utils
{
    public class CurrentClientResolver
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentClientResolver(IHttpContextAccessor contextAccessor)
        {
            this.httpContextAccessor = contextAccessor;
        }

        public long UserId
        {
            get { return GetUserId() ?? throw new Exception("UserId must be foundable in token"); }
        }

        public long CompanyId
        {
            get { return GetCompanyId() ?? throw new Exception("CompanyId must be foundable in token"); }
        }

        public List<Claim>? GetClaimsFromRequest()
        {
            try
            {
                Task<string?> tokenAsync = this.httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                tokenAsync.Wait();
                string authToken = tokenAsync.Result;

                if (!string.IsNullOrEmpty(authToken))
                {
                    return this.GetClaimsFromRequest(authToken);
                }

                return null;
            }
            catch (Exception exception)
            {
                throw new Exception("Fail in GetClaimsFromRequest()", exception);
            }
        }

        public List<Claim>? GetClaimsFromRequest(string authToken)
        {
            if (authToken != "undefined")
            {
                try
                {
                    JwtSecurityTokenHandler tokenHandler = new();
                    if (tokenHandler.ReadToken(authToken) is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Claims != null)
                    {
                        return jwtSecurityToken.Claims.ToList();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }

            return null;
        }

        public string GetUserName(List<Claim> claims = null)
        {
            if (claims == null || !claims.Any())
            {
                claims = this.GetClaimsFromRequest();
            }

            if (claims == null) return null;

            Claim companyClaim = claims.FirstOrDefault(c => c.Type == "name");

            if (companyClaim != null)
            {
                return companyClaim.Value;
            }

            return null;
        }

        public string? Get(string field, List<Claim> claims = null)
        {
            if (claims == null || !claims.Any())
            {
                claims = this.GetClaimsFromRequest();
            }

            if (claims == null) return null;

            Claim companyClaim = claims.FirstOrDefault(c => c.Type == field);

            if (companyClaim != null)
            {
                return companyClaim.Value;
            }

            return null;
        }

        public long? GetUserId()
        {
            bool converted = long.TryParse(Get("nameid"), out long userId);
            return converted && userId != 0 ? userId : null;
        }

        public long? GetCompanyId()
        {
            bool convertFromToken = long.TryParse(Get("companyId"), out long companyIdToken);

            long companyIdHeader = 0;
            bool convertFromHeader = convertFromToken ? false : long.TryParse(GetHeader($"companyId{this.httpContextAccessor.HttpContext.Request.Headers.RequestId}")[0], out companyIdHeader);
            return convertFromToken && companyIdToken != 0
                    ? companyIdToken
                    : (convertFromHeader && companyIdHeader != 0
                        ? companyIdHeader
                        : null);
        }

        public StringValues GetHeader(string headerName)
        {
            StringValues strings = StringValues.Empty;
            this.httpContextAccessor.HttpContext?.Request.Headers.TryGetValue(headerName, out strings);

            return strings;
        }
    }
}
