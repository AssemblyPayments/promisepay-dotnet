using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IMarketplaceRepository
    {
        IDictionary<string, object> ShowMarketplace();
    }
}
