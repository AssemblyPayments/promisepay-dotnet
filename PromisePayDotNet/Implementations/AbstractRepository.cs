using System;
using System.Collections;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Exceptions;
using RestSharp;

namespace PromisePayDotNet.Implementations
{
    public class AbstractRepository
    {
        protected const int EntityListLimit = 200;

        protected Hashtable Configurataion
        {
            get
            {
                var ht = ConfigurationManager.GetSection("PromisePay/Settings") as Hashtable;
                if (ht == null) throw new MisconfigurationException("Unable to get PromisePay settings section from config file");
                return ht;
            }
        }

        protected string BaseUrl
        {
            get
            {
                var baseUrl = Configurataion["ApiUrl"] as String;
                if (baseUrl == null) throw new MisconfigurationException("Unable to get URL info from config file");
                return baseUrl;
            }
        }

        protected string Login
        {
            get
            {
                var baseUrl = Configurataion["Login"] as String;
                if (baseUrl == null) throw new MisconfigurationException("Unable to get Login info from config file");
                return baseUrl;
               
            }
        }

        public string Password
        {
            get
            {
                var baseUrl = Configurataion["Password"] as String;
                if (baseUrl == null) throw new MisconfigurationException("Unable to get Password info from config file");
                return baseUrl;
            }
        }

        protected RestClient GetRestClient()
        {
            return new RestClient(BaseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(Login, Password)
            };
        }

        protected IRestResponse SendRequest(RestClient client, RestRequest request)
        {
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("Your login/password are unknown to server");
            }

            if (((int)response.StatusCode) == 422)
            {
                var errors = JsonConvert.DeserializeObject<ErrorsDAO>(response.Content).Errors;
                throw new ApiErrorsException("API returned errors, see Errors property", errors);
            }
            return response;
        }
    }
}
