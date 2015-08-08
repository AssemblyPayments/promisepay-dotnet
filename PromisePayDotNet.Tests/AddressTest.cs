using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Implementations;
using System.IO;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    public class AddressTest : AbstractTest
    {
        [TestMethod]
        public void AddressDeserialization()
        {
            var jsonStr = "{ \"addressline1\": null, \"addressline2\": null, \"postcode\": null, \"city\": null, \"state\": null, \"id\": \"07ed45e5-bb9d-459f-bb7b-a02ecb38f443\", \"country\": \"Australia\", \"links\": { \"self\": \"/addresses/07ed45e5-bb9d-459f-bb7b-a02ecb38f443\" }}";
            var address = JsonConvert.DeserializeObject<Address>(jsonStr);
            Assert.IsNotNull(address);
            Assert.AreEqual("07ed45e5-bb9d-459f-bb7b-a02ecb38f443", address.Id);
        }

        [TestMethod]
        public void GetAddressSuccessfully()
        {
            var content = File.ReadAllText("..\\..\\Fixtures\\address_get_by_id.json");

            var client = GetMockClient(content);

            var repo = new AddressRepository(client.Object);
            
            var address = repo.GetAddressById("07ed45e5-bb9d-459f-bb7b-a02ecb38f443");
            client.VerifyAll();
            Assert.IsNotNull(address);
            Assert.AreEqual("07ed45e5-bb9d-459f-bb7b-a02ecb38f443", address.Id);

        }
    }
}
