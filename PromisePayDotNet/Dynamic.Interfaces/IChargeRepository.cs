using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IChargeRepository
    {
        IDictionary<string,object> CreateCharge(IDictionary<string,object> charge);

        IDictionary<string, object> ListCharges(int limit = 10, int offset = 0);

        IDictionary<string, object> ShowCharge(string id);

        IDictionary<string, object> ShowChargeBuyer(string id);

        IDictionary<string, object> ShowChargeStatus(string id);
    }
}
