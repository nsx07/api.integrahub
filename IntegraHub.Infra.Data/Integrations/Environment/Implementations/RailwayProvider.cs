using IntegraHub.Infra.CrossCutting.Integrations.Environment.Base;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Infra.CrossCutting.Integrations.Environment.Implementations
{
    public class RailwayProvider : ProviderBase
    {
        public RailwayProvider() : base() { }
        public RailwayProvider(string endpoint, Method method) : base(endpoint, method) {}
        public override string ApiUrl => throw new NotImplementedException();

        public override void MountHeader()
        {
            throw new NotImplementedException();
        }
    }
}
