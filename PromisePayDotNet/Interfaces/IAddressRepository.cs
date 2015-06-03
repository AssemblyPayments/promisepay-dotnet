using PromisePayDotNet.DTO;

namespace PromisePayDotNet.Interfaces
{
    public interface IAddressRepository
    {
        Address GetAddressById(string addressId);
    }
}
