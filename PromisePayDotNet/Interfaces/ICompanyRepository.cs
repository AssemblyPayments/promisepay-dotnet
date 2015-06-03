using PromisePayDotNet.DTO;
using System.Collections.Generic;

namespace PromisePayDotNet.Interfaces
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> ListCompanies();

        Company GetCompanyById(string companyId);

        Company CreateCompany(Company company);

        Company EditCompany(Company company);
    }
}
