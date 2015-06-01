using PromisePayDotNet.DAO;
using System.Collections.Generic;

namespace PromisePayDotNet.Interfaces
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> ListCompanies();

        Company GetCompanyById(string companyId);
    }
}
