using IntegraHub.Infra.CrossCutting.Integrations.Environment.Base;
using IntegraHub.Infra.CrossCutting.Integrations.Environment.Utils;
using RestSharp;

namespace IntegraHub.Infra.CrossCutting.Integrations.Environment.Implementations
{
    public class VercelProvider: ProviderBase
    {
        public VercelProvider() : base() { }
        public VercelProvider(string endpoint, Method method) : base(endpoint, method) { }

        public override string ApiUrl => "https://api.vercel.com";

        public override void MountHeader()
        {
            Request.AddHeader("Authorization", $"Bearer {Tokens.Vercel}");
        }
    }
}
