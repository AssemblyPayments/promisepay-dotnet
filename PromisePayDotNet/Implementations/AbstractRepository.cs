using Newtonsoft.Json;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Exceptions;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace PromisePayDotNet.Implementations
{
    public class AbstractRepository
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected const int EntityListLimit = 200;

        protected Hashtable Configurataion
        {
            get
            {
                var ht = ConfigurationManager.GetSection("PromisePay/Settings") as Hashtable;
                if (ht == null)
                {
                    log.Fatal("Unable to get PromisePay settings section from config file");
                    throw new MisconfigurationException("Unable to get PromisePay settings section from config file");
                }
                return ht;
            }
        }

        protected string BaseUrl
        {
            get
            {
                var baseUrl = Configurataion["ApiUrl"] as String;
                if (baseUrl == null)
                {
                    log.Fatal("Unable to get URL info from config file");
                    throw new MisconfigurationException("Unable to get URL info from config file");
                }
                return baseUrl;
            }
        }

        protected string Login
        {
            get
            {
                var baseUrl = Configurataion["Login"] as String;
                if (baseUrl == null)
                {
                    log.Fatal("Unable to get Login info from config file");
                    throw new MisconfigurationException("Unable to get Login info from config file");
                }
                return baseUrl;
               
            }
        }

        public string Password
        {
            get
            {
                var baseUrl = Configurataion["Password"] as String;
                if (baseUrl == null)
                {
                    log.Fatal("Unable to get Password info from config file");
                    throw new MisconfigurationException("Unable to get Password info from config file");
                }
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

            log.Debug(String.Format(
                    "Executed request to {0} with method {1}, got the following status: {2} and the body is {3}",
                    response.ResponseUri, request.Method, response.StatusDescription, response.Content));

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                log.Error("Your login/password are unknown to server");
                throw new UnauthorizedException("Your login/password are unknown to server");
            }

            if (((int)response.StatusCode) == 422)
            {
                var errors = JsonConvert.DeserializeObject<ErrorsDAO>(response.Content).Errors;
                log.Error(String.Format("API returned following errors: {0}", JsonConvert.SerializeObject(errors)));
                throw new ApiErrorsException("API returned errors, see Errors property", errors);
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var message = JsonConvert.DeserializeObject<IDictionary<string,string>>(response.Content)["message"];
                log.Error(String.Format("Bad request: {0}", message));
                throw new ApiErrorsException(message, null);
            }
            return response;
        }

        protected void AssertIdNotNull(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                log.Error("id cannot be empty!");
                throw new ArgumentException("id cannot be empty!");
            }
        }

        protected void AssertListParamsCorrect(int limit, int offset)
        {
            if (limit < 0 || offset < 0)
            {
                log.Error("limit and offset values should be nonnegative!");
                throw new ArgumentException("limit and offset values should be nonnegative!");
            }

            if (limit > EntityListLimit)
            {
                var message = String.Format("Max value for limit parameter is {0}!", EntityListLimit);
                log.Error(message);
                throw new ArgumentException(message);
            }
        }
    }
}
