using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IBatchTransactionRepository
    {
        IDictionary<string, object> List(IDictionary<string, object> filter = null, int limit = 10, int offset = 0);

        IDictionary<string, object> Show(string id);
    }
}
