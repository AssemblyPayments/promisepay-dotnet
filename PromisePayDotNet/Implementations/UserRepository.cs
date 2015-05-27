using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Exceptions;
using PromisePayDotNet.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PromisePayDotNet.Implementations
{
    public class UserRepository : AbstractRepository, IUserRepository
    {
        #region public methods

        public IEnumerable<User> ListUsers(int limit = 10, int offset = 0)
        {
            AssertListParamsCorrect(limit, offset);
            var client = GetRestClient();
            var request = new RestRequest("/users", Method.GET);
            request.AddParameter("limit", limit);
            request.AddParameter("offset", offset);

            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("users"))
            {
                var userCollection = dict["users"];
                return JsonConvert.DeserializeObject<List<User>>(JsonConvert.SerializeObject(userCollection));
            }
            else
            {
                return new List<User>();
            }
        }

        public User GetUserById(string userId)
        {
            AssertIdNotNull(userId);
            var client = GetRestClient();
            var request = new RestRequest("/users/{id}", Method.GET);
            request.AddUrlSegment("id", userId);
            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, User>>(response.Content).Values.First();
        }

        public User CreateUser(User user)
        {
            ValidateUser(user);
            var client = GetRestClient();
            var request = new RestRequest("/users", Method.POST);
            request.AddParameter("id", user.Id);
            request.AddParameter("first_name", user.FirstName);
            request.AddParameter("last_name", user.LastName);
            request.AddParameter("email", user.Email);
            request.AddParameter("mobile", user.Mobile);
            request.AddParameter("address_line1", user.AddressLine1);
            request.AddParameter("address_line2", user.AddressLine2);
            request.AddParameter("state", user.State);
            request.AddParameter("city", user.City);
            request.AddParameter("zip", user.Zip);
            request.AddParameter("country", user.Country);

            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string,User>>(response.Content).Values.First();
        }

        public bool DeleteUser(string userId)
        {
            AssertIdNotNull(userId);
            var client = GetRestClient();
            var request = new RestRequest("/users/{id}", Method.DELETE);
            request.AddUrlSegment("id", userId);
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

        public void SendMobilePin(string userId)
        {
            AssertIdNotNull(userId);
            var client = GetRestClient();
            var request = new RestRequest("/users/:id/mobile_pin", Method.POST);
            request.AddUrlSegment("id", userId);
            var response = SendRequest(client, request);

        }

        public IEnumerable<Item> ListItemsForUser(string userId)
        {
            AssertIdNotNull(userId);
            var client = GetRestClient();
            var request = new RestRequest("/users/{id}/items", Method.GET);
            request.AddUrlSegment("id", userId);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("items"))
            {
                var itemCollection = dict["items"];
                return JsonConvert.DeserializeObject<List<Item>>(JsonConvert.SerializeObject(itemCollection));
            }
            else
            {
                return new List<Item>();
            }
        }

        public IEnumerable<PayPalAccount> ListPayPalAccountsForUser(string userId)
        {
            AssertIdNotNull(userId);
            var client = GetRestClient();
            var request = new RestRequest("/users/{id}/paypal_accounts", Method.GET);
            request.AddUrlSegment("id", userId);
            IRestResponse response;
            try
            {
                response = SendRequest(client, request);
            }
            catch (ApiErrorsException e)
            {
                if (e.Errors.Count == 1 && e.Errors.Values.First().First() == "no account found")
                {
                    return new List<PayPalAccount>();
                }
                throw e;
            }
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("paypal_accounts"))
            {
                var itemCollection = dict["paypal_accounts"];
                return JsonConvert.DeserializeObject<List<PayPalAccount>>(JsonConvert.SerializeObject(itemCollection));
            }
            else
            {
                return new List<PayPalAccount>();
            }
        }

        public IEnumerable<CardAccount> ListCardAccountsForUser(string userId)
        {
            AssertIdNotNull(userId);
            var client = GetRestClient();
            var request = new RestRequest("/users/{id}/card_accounts", Method.GET);
            request.AddUrlSegment("id", userId);
            IRestResponse response;
            try
            {
                response = SendRequest(client, request);
            }
            catch (ApiErrorsException e)
            {
                if (e.Errors.Count == 1 && e.Errors.Values.First().First() == "no account found")
                {
                    return new List<CardAccount>();
                }
                throw e;
            }
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("card_accounts"))
            {
                var itemCollection = dict["card_accounts"];
                return JsonConvert.DeserializeObject<List<CardAccount>>(JsonConvert.SerializeObject(itemCollection));
            }
            else
            {
                return new List<CardAccount>();
            }
        }

        public IEnumerable<BankAccount> ListBankAccountsForUser(string userId)
        {
            AssertIdNotNull(userId);
            var client = GetRestClient();
            var request = new RestRequest("/users/{id}/bank_accounts", Method.GET);
            request.AddUrlSegment("id", userId);
            IRestResponse response;
            try
            {
                response = SendRequest(client, request);
            }
            catch (ApiErrorsException e)
            {
                if (e.Errors.Count == 1 && e.Errors.Values.First().First() == "no account found")
                {
                    return new List<BankAccount>();
                }
                throw e;
            }
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("bank_accounts"))
            {
                var itemCollection = dict["bank_accounts"];
                return JsonConvert.DeserializeObject<List<BankAccount>>(JsonConvert.SerializeObject(itemCollection));
            }
            else
            {
                return new List<BankAccount>();
            }
        }

        public DisbursementAccount ListDisbursementAccounts(string userId, string accountId, string mobilePin)
        {
            throw new NotImplementedException();
            AssertIdNotNull(userId);
            var client = GetRestClient();
            var request = new RestRequest("/users/{id}/disbursement_account", Method.POST);
            request.AddUrlSegment("id", userId);
            request.AddUrlSegment("account_id", accountId);
            request.AddUrlSegment("mobile_pin", mobilePin);
            IRestResponse response;
            try
            {
                response = SendRequest(client, request);
            }
            catch (ApiErrorsException e)
            {
                throw e;
            }
        
        }

        public User UpdateUser(User user)
        {
            ValidateUser(user);
            var client = GetRestClient();
            var request = new RestRequest("/users/{id}", Method.PATCH);
            request.AddUrlSegment("id", user.Id);
            request.AddParameter("id", user.Id);
            request.AddParameter("first_name", user.FirstName);
            request.AddParameter("last_name", user.LastName);
            request.AddParameter("email", user.Email);
            request.AddParameter("mobile", user.Mobile);
            request.AddParameter("address_line1", user.AddressLine1);
            request.AddParameter("address_line2", user.AddressLine2);
            request.AddParameter("state", user.State);
            request.AddParameter("city", user.City);
            request.AddParameter("zip", user.Zip);
            request.AddParameter("country", user.Country);

            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, User>>(response.Content).Values.First();
        }
        #endregion

        #region private methods

        private void ValidateUser(User user)
        {
            if (String.IsNullOrEmpty(user.Id))
            {
                throw new ValidationException("Field User.ID should not be empty!");
            }
            if (String.IsNullOrEmpty(user.FirstName))
            {
                throw new ValidationException("Field User.FirstName should not be empty!");
            }
            if (!IsCorrectCountryCode(user.Country))
            {
                throw new ValidationException("Field User.Country should contain 3-letter ISO country code!");
            }
            if (!IsCorrectEmail(user.Email))
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

        private bool IsCorrectCountryCode(string countryCode)
        {
            return CountryCodes.Contains(countryCode.ToUpper());
        }

        private readonly List<string> CountryCodes = new List<string> { "AFG", "ALA", "ALB", "DZA", "ASM", "AND", "AGO", "AIA", "ATA", "ATG", "ARG", "ARM", "ABW", "AUS", "AUT", "AZE", "BHS", "BHR", "BGD", "BRB", "BLR", "BEL", "BLZ", "BEN", "BMU", "BTN", "BOL", "BIH", "BWA", "BVT", "BRA", "VGB", "IOT", "BRN", "BGR", "BFA", "BDI", "KHM", "CMR", "CAN", "CPV", "CYM", "CAF", "TCD", "CHL", "CHN", "HKG", "MAC", "CXR", "CCK", "COL", "COM", "COG", "COD", "COK", "CRI", "CIV", "HRV", "CUB", "CYP", "CZE", "DNK", "DJI", "DMA", "DOM", "ECU", "EGY", "SLV", "GNQ", "ERI", "EST", "ETH", "FLK", "FRO", "FJI", "FIN", "FRA", "GUF", "PYF", "ATF", "GAB", "GMB", "GEO", "DEU", "GHA", "GIB", "GRC", "GRL", "GRD", "GLP", "GUM", "GTM", "GGY", "GIN", "GNB", "GUY", "HTI", "HMD", "VAT", "HND", "HUN", "ISL", "IND", "IDN", "IRN", "IRQ", "IRL", "IMN", "ISR", "ITA", "JAM", "JPN", "JEY", "JOR", "KAZ", "KEN", "KIR", "PRK", "KOR", "KWT", "KGZ", "LAO", "LVA", "LBN", "LSO", "LBR", "LBY", "LIE", "LTU", "LUX", "MKD", "MDG", "MWI", "MYS", "MDV", "MLI", "MLT", "MHL", "MTQ", "MRT", "MUS", "MYT", "MEX", "FSM", "MDA", "MCO", "MNG", "MNE", "MSR", "MAR", "MOZ", "MMR", "NAM", "NRU", "NPL", "NLD", "ANT", "NCL", "NZL", "NIC", "NER", "NGA", "NIU", "NFK", "MNP", "NOR", "OMN", "PAK", "PLW", "PSE", "PAN", "PNG", "PRY", "PER", "PHL", "PCN", "POL", "PRT", "PRI", "QAT", "REU", "ROU", "RUS", "RWA", "BLM", "SHN", "KNA", "LCA", "MAF", "SPM", "VCT", "WSM", "SMR", "STP", "SAU", "SEN", "SRB", "SYC", "SLE", "SGP", "SVK", "SVN", "SLB", "SOM", "ZAF", "SGS", "SSD", "ESP", "LKA", "SDN", "SUR", "SJM", "SWZ", "SWE", "CHE", "SYR", "TWN", "TJK", "TZA", "THA", "TLS", "TGO", "TKL", "TON", "TTO", "TUN", "TUR", "TKM", "TCA", "TUV", "UGA", "UKR", "ARE", "GBR", "USA", "UMI", "URY", "UZB", "VUT", "VEN", "VNM", "VIR", "WLF", "ESH", "YEM", "ZMB", "ZWE" };

        #endregion
    }
}
