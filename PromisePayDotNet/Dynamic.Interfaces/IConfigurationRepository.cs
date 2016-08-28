using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IConfigurationRepository
    {
        IDictionary<string, object> Create(IDictionary<string, object> configuration);

        IDictionary<string, object> List();

        IDictionary<string, object> Show(string id);

        IDictionary<string, object> Update(IDictionary<string, object> configuration);

        IDictionary<string, object> Delete(string id);
    }
}
