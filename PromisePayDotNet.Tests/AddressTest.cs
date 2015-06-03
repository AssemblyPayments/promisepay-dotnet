using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Implementations;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    public class AddressTest
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
            var repo = new AddressRepository();
            var address = repo.GetAddressById("07ed45e5-bb9d-459f-bb7b-a02ecb38f443");
            Assert.IsNotNull(address);
            Assert.AreEqual("07ed45e5-bb9d-459f-bb7b-a02ecb38f443", address.Id);
        }
    }
}
