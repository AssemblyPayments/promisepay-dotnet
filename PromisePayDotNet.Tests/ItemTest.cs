using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Enums;
using PromisePayDotNet.Implementations;
using System;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    public class ItemTest
    {
        [TestMethod]
        public void TestDeserializeItem()
        {
            var jsonStr = "        {            \"id\": \"293\",            \"name\": \"Testing\",            \"description\": \"This is the description\",            \"created_at\": \"2015-05-01T04:42:25.595Z\",            \"updated_at\": \"2015-05-01T04:42:25.595Z\",            \"state\": \"pending\",            \"deposit_reference\": \"9140122157\",            \"payment_type_id\": 1,            \"status\": 22000,\"amount\": 6000,            \"buyer_name\": \"Joe Frio\",            \"buyer_country\": \"USA\",            \"buyer_email\": \"joe.test@promisepay.com\",\"seller_name\": \"Julie Boatsman\",            \"seller_country\": \"USA\",            \"seller_email\": \"julie.test@promisepay.com\",            \"currency\": \"USD\",\"links\": {                \"self\": \"/items/293\",                \"buyers\": \"/items/293/buyers\",                \"sellers\": \"/items/293/sellers\",                \"status\": \"/items/293/status\",                \"fees\": \"/items/293/fees\",                \"transactions\": \"/items/293/transactions\"            }        }";
            var item = JsonConvert.DeserializeObject<Item>(jsonStr);
            Assert.AreEqual("293", item.Id);
        }

        [TestMethod]
        public void CreateItemSuccessfully()
        {
            var repo = new ItemRepository();
            var id = Guid.NewGuid().ToString();
            var buyerId = "ec9bf096-c505-4bef-87f6-18822b9dbf2c"; //some user created before
            var sellerId = "fdf58725-96bd-4bf8-b5e6-9b61be20662e"; //some user created before
            var item = new Item
            {
                Id = id,
                Name = "Test Item #1",
                Amount = 1000,
                PaymentType = PaymentType.Express,
                BuyerId = buyerId, //optional field
                SellerId = sellerId, //optional field
                //No fee at this stage, optional field
                Description = "Test item #1 description"
            };
            var createdItem = repo.CreateItem(item);
            Assert.AreEqual(item.Id, createdItem.Id);
            Assert.AreEqual(item.Name, createdItem.Name);
            Assert.AreEqual(item.Amount, createdItem.Amount);
            Assert.AreEqual(item.PaymentType, createdItem.PaymentType);
            Assert.AreEqual(item.Description, createdItem.Description);
        }
    }
}
