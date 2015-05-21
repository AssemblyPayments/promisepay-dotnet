using System;
using System.Collections;
using System.Configuration;
using PromisePayDotNet.Exceptions;
using RestSharp;

namespace PromisePayDotNet.Implementations
{
    public class AbstractRepository
    {
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

    }
}
