using RestSharp;
using System;
using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class ConfigurationRepository : PromisePayDotNet.Implementations.AbstractRepository,
        PromisePayDotNet.Dynamic.Interfaces.IConfigurationRepository
    {
        public ConfigurationRepository(IRestClient client)
            : base(client)
        {
        }

        public IDictionary<string, object> Create(IDictionary<string, object> configuration)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> List()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> Show(string id)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> Update(IDictionary<string, object> configuration)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
