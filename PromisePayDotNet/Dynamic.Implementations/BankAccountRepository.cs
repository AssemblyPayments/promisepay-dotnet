﻿using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class BankAccountRepository : PromisePayDotNet.Implementations.AbstractRepository,
                                         PromisePayDotNet.Dynamic.Interfaces.IBankAccountRepository
    {
        public BankAccountRepository(IRestClient client)
            : base(client)
        {
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IDictionary<string, object> GetBankAccountById(string bankAccountId)
        {
            AssertIdNotNull(bankAccountId);
            var request = new RestRequest("/bank_accounts/{id}", Method.GET);
            request.AddUrlSegment("id", bankAccountId);
            var response = SendRequest(Client, request);
            var first = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content).Values.First();
            var firstJson = JsonConvert.SerializeObject(first);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(firstJson);
        }

        public IDictionary<string, object> CreateBankAccount(IDictionary<string, object> bankAccount)
        {
            var request = new RestRequest("/bank_accounts", Method.POST);
            request.AddParameter("user_id", (string)bankAccount["user_id"]);
            var bank = (IDictionary<string,object>)(bankAccount["bank"]);

            foreach (var key in bank.Keys) {
                request.AddParameter(key, (string)bank[key]);
            }

            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, IDictionary<string,object>>>(response.Content).Values.First();
        }

        public bool DeleteBankAccount(string bankAccountId)
        {
            AssertIdNotNull(bankAccountId);
            var request = new RestRequest("/bank_accounts/{id}", Method.DELETE);
            request.AddUrlSegment("id", bankAccountId);
            var response = SendRequest(Client, request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            return true;
        }

        public IDictionary<string, object> GetUserForBankAccount(string bankAccountId)
        {
            AssertIdNotNull(bankAccountId);
            var request = new RestRequest("/bank_accounts/{id}/users", Method.GET);
            request.AddUrlSegment("id", bankAccountId);
            IRestResponse response = SendRequest(Client, request);

            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("users"))
            {
                var item = dict["users"];
                return JsonConvert.DeserializeObject<IDictionary<string, object>>(JsonConvert.SerializeObject(item));
            }
            return null;
        }
    }

}
