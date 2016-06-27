using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IFeeRepository
    {

        IDictionary<string, object> ListFees();

        IDictionary<string, object> GetFeeById(string feeId);

        IDictionary<string, object> CreateFee(IDictionary<string, object> fee);

    }
}
