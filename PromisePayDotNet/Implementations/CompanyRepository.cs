using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace PromisePayDotNet.Implementations
{
    public class CompanyRepository : AbstractRepository, ICompanyRepository
    {
        public IEnumerable<Company> ListCompanies()
        {
            var client = GetRestClient();
            var request = new RestRequest("/companies", Method.GET);

            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("companies"))
            {
                var uploadCollection = dict["companies"];
                return JsonConvert.DeserializeObject<List<Company>>(JsonConvert.SerializeObject(uploadCollection));
            }
            return new List<Company>();
        }

        public Company GetCompanyById(string companyId)
        {
            AssertIdNotNull(companyId);
            var client = GetRestClient();
            var request = new RestRequest("/companies/{id}", Method.GET);
            request.AddUrlSegment("id", companyId);
            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, Company>>(response.Content).Values.First();
        }

        public Company CreateCompany(Company company)
        {
            var client = GetRestClient();
            var request = new RestRequest("/companies", Method.POST);
            request.AddParameter("name", company.Name);
            request.AddParameter("legal_name", company.LegalName);
            request.AddParameter("tax_number", company.TaxNumber);
            request.AddParameter("charge_tax", company.ChargeTax);
            request.AddParameter("address_line1", company.AddressLine1);
            request.AddParameter("address_line2", company.AddressLine2);
            request.AddParameter("city", company.City);
            request.AddParameter("state", company.State);
            request.AddParameter("zip", company.Zip);
            request.AddParameter("country", company.Country);
            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, Company>>(response.Content).Values.First();
        }

        public Company EditCompany(Company company)
        {
            var client = GetRestClient();
            var request = new RestRequest("/companies", Method.POST);
            request.AddParameter("name", company.Name);
            request.AddParameter("legal_name", company.LegalName);
            request.AddParameter("tax_number", company.TaxNumber);
            request.AddParameter("charge_tax", company.ChargeTax);
            request.AddParameter("address_line1", company.AddressLine1);
            request.AddParameter("address_line2", company.AddressLine2);
            request.AddParameter("city", company.City);
            request.AddParameter("state", company.State);
            request.AddParameter("zip", company.Zip);
            request.AddParameter("country", company.Country);
            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, Company>>(response.Content).Values.First();
        }

    }
}
