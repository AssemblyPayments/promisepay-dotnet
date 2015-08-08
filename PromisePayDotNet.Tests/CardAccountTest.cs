using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Implementations;
using System;
using System.IO;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    public class CardAccountTest : AbstractTest
    {
        [TestMethod]
        public void CardAccountDeserialization()
        {
            var jsonStr = "{ \"active\": true, \"created_at\": \"2015-04-26T06:28:55.559Z\", \"updated_at\": \"2015-04-26T06:28:55.559Z\", \"id\": \"ea464d25-fc9a-4887-861a-3d8ec2e12c19\", \"currency\": \"USD\", \"card\": { \"type\": \"visa\", \"full_name\": \"Joe Frio\", \"number\": \"XXXX-XXXX-XXXX-1111\", \"expiry_month\": \"5\", \"expiry_year\": \"2016\" }, \"links\": { \"self\": \"/card_accounts/ea464d25-fc9a-4887-861a-3d8ec2e12c19\", \"users\": \"/card_accounts/ea464d25-fc9a-4887-861a-3d8ec2e12c19/users\" } }";
            var cardAccount = JsonConvert.DeserializeObject<CardAccount>(jsonStr);
            Assert.AreEqual("ea464d25-fc9a-4887-861a-3d8ec2e12c19", cardAccount.Id);
            Assert.AreEqual("USD", cardAccount.Currency);
            Assert.AreEqual("Joe Frio", cardAccount.Card.FullName);
        }

        [TestMethod]
        public void CreateCardAccountSuccessfully()
        {
            var content = File.ReadAllText("..\\..\\Fixtures\\card_account_create.json");

            var client = GetMockClient(content);
            var repo = new CardAccountRepository(client.Object);

            var userId = "ec9bf096-c505-4bef-87f6-18822b9dbf2c"; //some user created before
            var account = new CardAccount
            {
                UserId = userId,
                Active = true,
                Card = new Card
                {
                    FullName = "Batman",
                    ExpiryMonth = "11",
                    ExpiryYear = "2020",
                    Number = "4111111111111111",
                    Type = "visa",
                    CVV = "123"
                }
            };
            var createdAccount = repo.CreateCardAccount(account);
            client.VerifyAll();
            Assert.IsNotNull(createdAccount);
            Assert.IsNotNull(createdAccount.Id);
            Assert.AreEqual("AUD", createdAccount.Currency); // It seems that currency is determined by country
            Assert.IsNotNull(createdAccount.CreatedAt);
            Assert.IsNotNull(createdAccount.UpdatedAt);
           
        }

        [TestMethod]
        public void GetCardAccountSuccessfully()
        {
            var content = File.ReadAllText("..\\..\\Fixtures\\card_account_get_by_id.json");

            var client = GetMockClient(content);
            var repo = new CardAccountRepository(client.Object);
            var gotAccount = repo.GetCardAccountById("25d34744-8ef0-46a4-8b18-2a8322933cd1");
            client.VerifyAll();
            Assert.AreEqual("25d34744-8ef0-46a4-8b18-2a8322933cd1", gotAccount.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCardAccountEmptyId()
        {
            var client = GetMockClient("");
            var repo = new CardAccountRepository(client.Object);
            repo.GetCardAccountById(string.Empty);
        }

        [TestMethod]
        public void GetUserForCardAccountSuccessfully()
        {
            var content = File.ReadAllText("..\\..\\Fixtures\\card_account_get_users.json");

            var client = GetMockClient(content);
            var repo = new CardAccountRepository(client.Object);
            var gotUser = repo.GetUserForCardAccount("25d34744-8ef0-46a4-8b18-2a8322933cd1");

            client.VerifyAll();

            Assert.IsNotNull(gotUser);
            Assert.AreEqual("1", gotUser.Id);
        }

        [TestMethod]
        public void DeleteCardAccountSuccessfully()
        {
            var content = File.ReadAllText("..\\..\\Fixtures\\card_account_delete.json");

            var client = GetMockClient(content);
            var repo = new CardAccountRepository(client.Object);
            var id = "25d34744-8ef0-46a4-8b18-2a8322933cd1";

            var result = repo.DeleteCardAccount(id);
            client.VerifyAll();
            Assert.IsTrue(result);
        }

    }
}
