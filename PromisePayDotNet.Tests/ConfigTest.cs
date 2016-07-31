using NUnit.Framework;
using System;
using System.Collections;
using System.Configuration;

namespace PromisePayDotNet.Tests
{
    public class ConfigTest
    {
        [Test]
        public void TestPromisePayConfig()
        {
            var ht = ConfigurationManager.GetSection("PromisePay/Settings") as Hashtable;
            if (ht != null)
            {
                var keyVal = ht["ApiUrl"] as String;
                Assert.AreEqual("https://test.api.promisepay.com", keyVal, "App.config section is wrong");
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestAlternativeConfig() 
        {
            var apiUrl = ConfigurationManager.AppSettings["PromisePayApiUrl"] as string;

            Assert.AreEqual("https://test.api.promisepay.com", apiUrl);
        }
    }
}
