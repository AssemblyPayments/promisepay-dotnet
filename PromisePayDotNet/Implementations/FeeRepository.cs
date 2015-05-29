using PromisePayDotNet.DAO;
using PromisePayDotNet.Interfaces;
using System;
using System.Collections.Generic;

namespace PromisePayDotNet.Implementations
{
    public class FeeRepository : AbstractRepository, IFeeRepository
    {
        public IEnumerable<Fee> ListFees()
        {
            throw new NotImplementedException();
        }

        public Fee GetFeeById(string feeId)
        {
            throw new NotImplementedException();
        }

        public Fee CreateFee(Fee fee)
        {
            throw new NotImplementedException();
        }
    }
}
