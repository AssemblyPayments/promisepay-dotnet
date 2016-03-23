﻿using Newtonsoft.Json;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Exceptions;
using RestSharp;
using RestSharp.Authenticators;
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

        protected IRestClient Client;

        public AbstractRepository(IRestClient client)
        {
            this.Client = client;
            client.BaseUrl = new Uri(BaseUrl);
            client.Authenticator = new HttpBasicAuthenticator(Login, Password);
        }

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

        protected IRestResponse SendRequest(IRestClient client, IRestRequest request)
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

        protected bool IsCorrectCountryCode(string countryCode)
        {
            return _countryCodes.Contains(countryCode.ToUpper());
        }

        private readonly List<string> _countryCodes = new List<string> { "AFG", "ALA", "ALB", "DZA", "ASM", "AND", "AGO", "AIA", "ATA", "ATG", "ARG", "ARM", "ABW", "AUS", "AUT", "AZE", "BHS", "BHR", "BGD", "BRB", "BLR", "BEL", "BLZ", "BEN", "BMU", "BTN", "BOL", "BIH", "BWA", "BVT", "BRA", "VGB", "IOT", "BRN", "BGR", "BFA", "BDI", "KHM", "CMR", "CAN", "CPV", "CYM", "CAF", "TCD", "CHL", "CHN", "HKG", "MAC", "CXR", "CCK", "COL", "COM", "COG", "COD", "COK", "CRI", "CIV", "HRV", "CUB", "CYP", "CZE", "DNK", "DJI", "DMA", "DOM", "ECU", "EGY", "SLV", "GNQ", "ERI", "EST", "ETH", "FLK", "FRO", "FJI", "FIN", "FRA", "GUF", "PYF", "ATF", "GAB", "GMB", "GEO", "DEU", "GHA", "GIB", "GRC", "GRL", "GRD", "GLP", "GUM", "GTM", "GGY", "GIN", "GNB", "GUY", "HTI", "HMD", "VAT", "HND", "HUN", "ISL", "IND", "IDN", "IRN", "IRQ", "IRL", "IMN", "ISR", "ITA", "JAM", "JPN", "JEY", "JOR", "KAZ", "KEN", "KIR", "PRK", "KOR", "KWT", "KGZ", "LAO", "LVA", "LBN", "LSO", "LBR", "LBY", "LIE", "LTU", "LUX", "MKD", "MDG", "MWI", "MYS", "MDV", "MLI", "MLT", "MHL", "MTQ", "MRT", "MUS", "MYT", "MEX", "FSM", "MDA", "MCO", "MNG", "MNE", "MSR", "MAR", "MOZ", "MMR", "NAM", "NRU", "NPL", "NLD", "ANT", "NCL", "NZL", "NIC", "NER", "NGA", "NIU", "NFK", "MNP", "NOR", "OMN", "PAK", "PLW", "PSE", "PAN", "PNG", "PRY", "PER", "PHL", "PCN", "POL", "PRT", "PRI", "QAT", "REU", "ROU", "RUS", "RWA", "BLM", "SHN", "KNA", "LCA", "MAF", "SPM", "VCT", "WSM", "SMR", "STP", "SAU", "SEN", "SRB", "SYC", "SLE", "SGP", "SVK", "SVN", "SLB", "SOM", "ZAF", "SGS", "SSD", "ESP", "LKA", "SDN", "SUR", "SJM", "SWZ", "SWE", "CHE", "SYR", "TWN", "TJK", "TZA", "THA", "TLS", "TGO", "TKL", "TON", "TTO", "TUN", "TUR", "TKM", "TCA", "TUV", "UGA", "UKR", "ARE", "GBR", "USA", "UMI", "URY", "UZB", "VUT", "VEN", "VNM", "VIR", "WLF", "ESH", "YEM", "ZMB", "ZWE" };

    }
}
