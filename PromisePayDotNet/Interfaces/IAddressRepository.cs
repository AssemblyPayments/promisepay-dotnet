using PromisePayDotNet.DAO;

namespace PromisePayDotNet.Interfaces
{
    public interface IAddressRepository
    {
        Address GetAddressById(string addressId);
    }
}
