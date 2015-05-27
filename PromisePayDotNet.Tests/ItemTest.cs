using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Enums;
using PromisePayDotNet.Exceptions;
using PromisePayDotNet.Implementations;
using System;
using System.Linq;

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

        [TestMethod]
        public void ListAllItemsSuccessfully()
        {
            var repo = new ItemRepository();
            //Then, list items
            var items = repo.ListItems(200);

            Assert.IsNotNull(items);
            Assert.IsTrue(items.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ListAllItemsNegativeParams()
        {
            var repo = new ItemRepository();
            //Then, list items
            var items = repo.ListItems(-10,-10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ListAllItemsTooHighLimit()
        {
            var repo = new ItemRepository();
            //Then, list items
            var items = repo.ListItems(500);
        }

        [TestMethod]
        public void GetItemSuccessful()
        {
            //First, create a user with known id
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

            repo.CreateItem(item);

            //Then, get user
            var gotItem = repo.GetItemById(id);

            Assert.IsNotNull(gotItem);
            Assert.AreEqual(gotItem.Id, id);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        //That's bad idea not to distinguish between "wrong login/password" and "There is no such ID in DB"
        public void GetItemMissingId()
        {
            var repo = new ItemRepository();
            var id = Guid.NewGuid().ToString();
            repo.GetItemById(id);
        }

        [TestMethod]
        public void DeleteItemSuccessful()
        {
            //First, create a item with known id
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

            repo.CreateItem(item);

            //Then, get item
            var gotItem = repo.GetItemById(id);
            Assert.IsNotNull(gotItem);
            Assert.AreEqual(gotItem.Id, id);

            //Now, delete item
            Assert.IsTrue(repo.DeleteItem(id));

            //And check whether item exists now

            var deletedItem = repo.GetItemById(id);

            //Exists, but unactive
            Assert.AreEqual("cancelled",deletedItem.State);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        //That's bad idea not to distinguish between "wrong login/password" and "There is no such ID in DB"
        public void DeleteItemMissingId()
        {
            var repo = new ItemRepository();
            var id = Guid.NewGuid().ToString();
            Assert.IsFalse(repo.DeleteItem(id));
        }

        [TestMethod]
        public void EditItemSuccessful()
        {
            //First, create a item we'll work with
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

            repo.CreateItem(item);

            //Now, try to edit newly created item
            item.Name = "Test123";
            item.Description = "Test123";
            var updatedItem = repo.UpdateItem(item);

            Assert.AreEqual("Test123", updatedItem.Name);
            Assert.AreEqual("Test123", updatedItem.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public void EditItemMissingId()
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

            repo.UpdateItem(item);
        }

        [TestMethod]
        public void ListTransactionsForItem()
        {
            var repo = new ItemRepository();
            var transactions = repo.ListTransactionsForItem("7c269f52-2236-4aa5-899e-a2e3ecadbc3f");
        }

        [TestMethod]
        public void GetStatusForItem()
        {
            var repo = new ItemRepository();
            var status = repo.GetStatusForItem("7c269f52-2236-4aa5-899e-a2e3ecadbc3f");
            Assert.IsNotNull(status);
        }

        [TestMethod]
        public void ListFeesForItem()
        {
            var repo = new ItemRepository();
            var fees = repo.ListFeesForItem("7c269f52-2236-4aa5-899e-a2e3ecadbc3f");
            
        }

        [TestMethod]
        public void GetBuyerForItemSuccessfully()
        {
            var repo = new ItemRepository();
            var buyer = repo.GetBuyerForItem("7c269f52-2236-4aa5-899e-a2e3ecadbc3f");
            Assert.IsNotNull(buyer);
        }

        [TestMethod]
        public void GetSellerForItemSuccessfully()
        {
            var repo = new ItemRepository();
            var sellers = repo.GetSellerForItem("7c269f52-2236-4aa5-899e-a2e3ecadbc3f");
            Assert.IsNotNull(sellers);
        }


        [TestMethod]
        public void GetWireDetailsForItemSuccessfully()
        {
            var repo = new ItemRepository();
            var wireDetails = repo.GetWireDetailsForItem("7c269f52-2236-4aa5-899e-a2e3ecadbc3f");
            Assert.IsNotNull(wireDetails);
        }

        [TestMethod]
        public void GetBPayDetailsForItemSuccessfully()
        {
            var repo = new ItemRepository();
            var bPayDetails = repo.GetBPayDetailsForItem("7c269f52-2236-4aa5-899e-a2e3ecadbc3f");
            Assert.IsNotNull(bPayDetails);
        }
    }
}
