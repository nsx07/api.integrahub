using IntegraHub.Infra.CrossCutting.Integrations.Environment.Utils;
using RestSharp;

namespace IntegraHub.Infra.CrossCutting.Integrations.Environment.Base
{
    public abstract class ProviderBase : IDisposable
    {
        public abstract string ApiUrl { get; }
        public RestClient Client { get; set; }
        public RestRequest Request { get; set; }
        protected Tokens Tokens = TokenHandler.Resolve();

        public ProviderBase()
        {
            Client = new RestClient(ApiUrl);
            Request = new RestRequest();
            MountHeader();
        }

        public ProviderBase(string endpoint, Method method)
        {
            Request = new RestRequest(endpoint, method);
            Client = new RestClient(ApiUrl);
            MountHeader();
        }

        public abstract void MountHeader();

        public void Dispose()
        {
            this.Client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
