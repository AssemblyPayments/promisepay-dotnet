using Newtonsoft.Json;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace PromisePayDotNet.Implementations
{
    public class CompanyRepository : AbstractRepository, ICompanyRepository
    {
        public CompanyRepository(IRestClient client) : base(client)
        {
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IEnumerable<Company> ListCompanies()
        {
            var request = new RestRequest("/companies", Method.GET);

            var response = SendRequest(Client, request);
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
            var request = new RestRequest("/companies/{id}", Method.GET);
            request.AddUrlSegment("id", companyId);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, Company>>(response.Content).Values.First();
        }

        public Company CreateCompany(Company company)
        {
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
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, Company>>(response.Content).Values.First();
        }

        public Company EditCompany(Company company)
        {
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
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, Company>>(response.Content).Values.First();
        }

    }
}
