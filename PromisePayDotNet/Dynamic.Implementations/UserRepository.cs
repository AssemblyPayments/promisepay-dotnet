using Newtonsoft.Json;
using PromisePayDotNet.Exceptions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class UserRepository : PromisePayDotNet.Implementations.AbstractRepository,
                                  PromisePayDotNet.Dynamic.Interfaces.IUserRepository
    {
        public UserRepository(IRestClient client)
            : base(client)
        {
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region public methods

        public IDictionary<string,object> ListUsers(int limit = 10, int offset = 0)
        {
            AssertListParamsCorrect(limit, offset);
            var request = new RestRequest("/users", Method.GET);
            request.AddParameter("limit", limit);
            request.AddParameter("offset", offset);

            var response = SendRequest(Client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            return dict;
        }

        public IDictionary<string,object> GetUserById(string userId)
        {
            AssertIdNotNull(userId);
            var request = new RestRequest("/users/{id}", Method.GET);
            request.AddUrlSegment("id", userId);
            var response = SendRequest(Client, request);
            var returnedUser = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content).Values.First();
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(JsonConvert.SerializeObject(returnedUser));
        }

        public IDictionary<string,object> CreateUser(IDictionary<string,object> user)
        {
            ValidateUser(user);
            var request = new RestRequest("/users", Method.POST);

            foreach (var key in user.Keys) {
                request.AddParameter(key, (string)user[key]);
            }
            
            var response = SendRequest(Client, request);
            var returnedUser = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content).Values.First();
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(JsonConvert.SerializeObject(returnedUser));
        }

        public bool DeleteUser(string userId)
        {
            AssertIdNotNull(userId);
            var request = new RestRequest("/users/{id}", Method.DELETE);
            request.AddUrlSegment("id", userId);
            var response = SendRequest(Client, request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            return true;
        }

        public IDictionary<string,object> ListItemsForUser(string userId)
        {
            AssertIdNotNull(userId);
            var request = new RestRequest("/users/{id}/items", Method.GET);
            request.AddUrlSegment("id", userId);
            var response = SendRequest(Client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            return dict;
        }

        public IDictionary<string,object> GetPayPalAccountForUser(string userId)
        {
            AssertIdNotNull(userId);
            var request = new RestRequest("/users/{id}/paypal_accounts", Method.GET);
            request.AddUrlSegment("id", userId);
            IRestResponse response;
            try
            {
                response = SendRequest(Client, request);
            }
            catch (ApiErrorsException e)
            {
                if (e.Errors.Count == 1 && e.Errors.Values.First().First() == "no account found")
                {
                    return null;
                }
                throw;
            }
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            return dict;
        }

        public IDictionary<string,object> GetCardAccountForUser(string userId)
        {
            AssertIdNotNull(userId);
            var request = new RestRequest("/users/{id}/card_accounts", Method.GET);
            request.AddUrlSegment("id", userId);
            IRestResponse response;
            try
            {
                response = SendRequest(Client, request);
            }
            catch (ApiErrorsException e)
            {
                if (e.Errors.Count == 1 && e.Errors.Values.First().First() == "no account found")
                {
                    return null;
                }
                throw;
            }
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            return dict;
        }

        public IDictionary<string,object> GetBankAccountForUser(string userId)
        {
            AssertIdNotNull(userId);
            var request = new RestRequest("/users/{id}/bank_accounts", Method.GET);
            request.AddUrlSegment("id", userId);
            IRestResponse response;
            try
            {
                response = SendRequest(Client, request);
            }
            catch (ApiErrorsException e)
            {
                if (e.Errors.Count == 1 && e.Errors.Values.First().First() == "no account found")
                {
                    return new Dictionary<string,object>();
                }
                throw e;
            }
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("bank_accounts"))
            {
                var itemCollection = dict["bank_accounts"];
                return JsonConvert.DeserializeObject<IDictionary<string,object>>(JsonConvert.SerializeObject(itemCollection));
            }

            return new Dictionary<string,object>();
        }

        public bool SetDisbursementAccount(string userId, string accountId)
        {
            //ToDo find out DisbursementAccount fields and implement this method 
            throw new NotImplementedException();
            //            AssertIdNotNull(userId);
            //
            //            var request = new RestRequest("/users/{id}/disbursement_account", Method.POST);
            //            request.AddUrlSegment("id", userId);
            //            request.AddUrlSegment("account_id", accountId);
            //            try
            //            {
            //                SendRequest(Client, request);
            //            }
            //            catch (ApiErrorsException e)
            //            {
            //                throw;
            //            }

        }

        public IDictionary<string,object> UpdateUser(IDictionary<string,object> user)
        {
            ValidateUser(user);
            var request = new RestRequest("/users/{id}", Method.PATCH);
            request.AddUrlSegment("id", (string)user["id"]);

            foreach (var key in user.Keys)
            {
                request.AddParameter(key, (string)user[key]);
            }
            var response = SendRequest(Client, request);
            var returnedUser = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content).Values.First();
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(JsonConvert.SerializeObject(returnedUser));
        }

        #endregion

        #region private methods

        private void ValidateUser(IDictionary<string,object> user)
        {
            if ((!user.ContainsKey("id")) || String.IsNullOrEmpty((string)user["id"]))
            {
                throw new ValidationException("Field User.ID should not be empty!");
            }
            if ((!user.ContainsKey("first_name")) || String.IsNullOrEmpty((string)user["first_name"]))
            {
                throw new ValidationException("Field User.FirstName should not be empty!");
            }
            if ((!user.ContainsKey("country")) || !IsCorrectCountryCode((string)user["country"]))
            {
                throw new ValidationException("Field User.Country should contain 3-letter ISO country code!");
            }
            if ((!user.ContainsKey("email")) || !IsCorrectEmail((string)user["email"]))
            {
                throw new ValidationException("Field User.Email should contain correct email address!");
            }
        }

        private bool IsCorrectEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        #endregion

    }
}
