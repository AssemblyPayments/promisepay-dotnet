using PromisePayDotNet.DAO;
using System.Collections.Generic;

namespace PromisePayDotNet.Interfaces
{
    public interface IFeeRepository
    {

        IEnumerable<Fee> ListFees();

        Fee GetFeeById(string feeId);

        Fee CreateFee(Fee fee);

    }
}
