using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IToolRepository
    {
        IDictionary<string, object> HealthCheck();
    }
}
