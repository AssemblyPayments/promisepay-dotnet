using System.Security.Cryptography.X509Certificates;
using PromisePayDotNet.DAO;

namespace PromisePayDotNet.Interfaces
{
    public interface IUserRepository
    {
        User GetUserById(string id);
    }
}
