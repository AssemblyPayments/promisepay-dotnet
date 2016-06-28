using System.Collections.Generic;
namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IDirectDebitAuthorityRepository
    {
        IDictionary<string, object> Create(string accountId, string amount);

        IDictionary<string, object> List(string accountId, int limit=10, int offset=0);

        IDictionary<string, object> Show(string id);

        IDictionary<string, object> Delete(string id);
    }
}
