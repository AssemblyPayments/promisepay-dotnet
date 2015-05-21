using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Exceptions;
using PromisePayDotNet.Interfaces;
using RestSharp;

namespace PromisePayDotNet.Implementations
{
    public class UserRepository : AbstractRepository, IUserRepository
    {
        public IEnumerable<User> ListUsers(int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(string userId)
        {
            throw new NotImplementedException();
        }

        public User CreateUser(User user)
        {
            var client = GetRestClient();
            var request = new RestRequest("/users", Method.POST);
            request.AddParameter("id", user.ID);
            request.AddParameter("first_name", user.FirstName);
            request.AddParameter("last_name", user.LastName);
            request.AddParameter("email", user.Email);
            request.AddParameter("mobile", user.MobileName);
            request.AddParameter("address_line1", user.AddressLine1);
            request.AddParameter("address_line2", user.AddressLine2);
            request.AddParameter("state", user.State);
            request.AddParameter("city", user.City);
            request.AddParameter("zip", user.Zip);
            request.AddParameter("country", user.Country);

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("Your login/password are unknown to server");
            }

            if (((int) response.StatusCode) == 422)
            {
                var errors = JsonConvert.DeserializeObject<ErrorsDAO>(response.Content).Errors;
                throw new ApiErrorsException("API returned errors, see Errors property", errors);
            }
            return JsonConvert.DeserializeObject<IDictionary<string,User>>(response.Content).Values.First();
        }

        public void DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public void SendMobilePin(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> ListItemsForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PayPalAccount> ListPayPalAccountsForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CardAccount> ListCardAccountsForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BankAccount> ListBankAccountsForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public DisbursementAccount ListDisbursementAccounts(string userId, string accountId, string mobilePin)
        {
            throw new NotImplementedException();
        }
    }
}
