using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IRestrictionRepository
    {
        IDictionary<string, object> List();

        IDictionary<string, object> Show(string id);
    }
}
