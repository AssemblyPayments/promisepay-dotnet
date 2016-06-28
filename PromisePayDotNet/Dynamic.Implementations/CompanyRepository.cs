﻿using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class CompanyRepository : PromisePayDotNet.Implementations.AbstractRepository,
                                         PromisePayDotNet.Dynamic.Interfaces.ICompanyRepository
    {
        public CompanyRepository(IRestClient client)
            : base(client)
        {
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IDictionary<string,object> ListCompanies()
        {
            var request = new RestRequest("/companies", Method.GET);

            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }

        public IDictionary<string, object> GetCompanyById(string companyId)
        {
            AssertIdNotNull(companyId);
            var request = new RestRequest("/companies/{id}", Method.GET);
            request.AddUrlSegment("id", companyId);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }

        public IDictionary<string, object> CreateCompany(IDictionary<string, object> company)
        {
            var request = new RestRequest("/companies", Method.POST);

            foreach (var key in company.Keys) {
                request.AddParameter(key, (string)company[key]);
            }
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }

        public IDictionary<string, object> EditCompany(IDictionary<string, object> company)
        {
            var request = new RestRequest("/companies", Method.POST);

            foreach (var key in company.Keys) {
                request.AddParameter(key, (string)company[key]);
            }

            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }
    }
}
