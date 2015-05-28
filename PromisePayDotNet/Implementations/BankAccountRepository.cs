using System.Net;
using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromisePayDotNet.Implementations
{
    public class BankAccountRepository : AbstractRepository, IBankAccountRepository
    {
        public BankAccount GetBankAccountById(string bankAccountId)
        {
            AssertIdNotNull(bankAccountId);
            var client = GetRestClient();
            var request = new RestRequest("/bank_accounts/{id}", Method.GET);
            request.AddUrlSegment("id", bankAccountId);
            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, BankAccount>>(response.Content).Values.First();
        }

        public BankAccount CreateBankAccount(BankAccount bankAccount, string mobilePin = "")
        {
            var client = GetRestClient();
            var request = new RestRequest("/bank_accounts", Method.POST);
            request.AddParameter("user_id", bankAccount.UserId);
            request.AddParameter("bank_name", bankAccount.Bank.BankName);
            request.AddParameter("account_name", bankAccount.Bank.AccountName);
            request.AddParameter("routing_number", bankAccount.Bank.RoutingNumber);
            request.AddParameter("account_number", bankAccount.Bank.AccountNumber);
            request.AddParameter("account_type", bankAccount.Bank.AccountType);
            request.AddParameter("holder_type", bankAccount.Bank.HolderType);
            request.AddParameter("country", bankAccount.Bank.Country);
            request.AddParameter("mobile_pin", mobilePin);
            
            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, BankAccount>>(response.Content).Values.First();
        }

        public bool DeleteBankAccount(string bankAccountId, string mobilePin = "")
        {
            AssertIdNotNull(bankAccountId);
            var client = GetRestClient();
            var request = new RestRequest("/bank_accounts/{id}", Method.DELETE);
            request.AddUrlSegment("id", bankAccountId);
            request.AddParameter("mobile_pin", mobilePin);
            var response = SendRequest(client, request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public User GetUserForBankAccount(string bankAccountId)
        {
            AssertIdNotNull(bankAccountId);
            var client = GetRestClient();
            var request = new RestRequest("/bank_accounts/{id}/users", Method.GET);
            request.AddUrlSegment("id", bankAccountId);
            IRestResponse response;
            response = SendRequest(client, request);

            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("users"))
            {
                var item = dict["users"];
                return JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(item));
            }
            else
            {
                return null;
            }            
        }
    }
}
