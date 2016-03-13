using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IAddressRepository
    {
        IDictionary<string,object> GetAddressById(string addressId);
    }
}
