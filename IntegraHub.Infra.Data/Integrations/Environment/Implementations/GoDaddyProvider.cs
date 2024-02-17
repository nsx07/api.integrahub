using IntegraHub.Infra.CrossCutting.Integrations.Environment.Base;
using RestSharp;

namespace IntegraHub.Infra.CrossCutting.Integrations.Environment.Implementations
{
    public class GoDaddyProvider : ProviderBase
    {
        public GoDaddyProvider(): base() { }
        public GoDaddyProvider(string endpoint, Method method) : base(endpoint, method) { }
        public override string ApiUrl => "https://api.godaddy.com";

        public override void MountHeader()
        {
            Request.AddHeader("Authorization", $"sso-key {Tokens.GoDaddyKey}:{Tokens.GoDaddySecret}");
        }
    }
}
