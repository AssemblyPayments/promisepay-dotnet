using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Implementations;
using System;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    public class CardAccountTest
    {
        [TestMethod]
        public void TestDeserialization()
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
            var repo = new CardAccountRepository();
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
            Assert.IsNotNull(createdAccount);
            Assert.IsNotNull(createdAccount.Id);
            Assert.AreEqual("AUD", createdAccount.Currency); // It seems that currency is determined by country
            Assert.IsNotNull(createdAccount.CreatedAt);
            Assert.IsNotNull(createdAccount.UpdatedAt);
           
        }

        [TestMethod]
        public void GetCardAccountSuccessfully()
        {
            var repo = new CardAccountRepository();
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

            var gotAccount = repo.GetCardAccountById(createdAccount.Id);

            Assert.AreEqual(createdAccount.Id, gotAccount.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBankAccountEmptyId()
        {
            var repo = new CardAccountRepository();
            repo.GetCardAccountById(string.Empty);
        }

        [TestMethod]
        public void GetUserForCardAccountSuccessfully()
        {
            var repo = new CardAccountRepository();
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

            var gotUser = repo.GetUserForCardAccount(createdAccount.Id);

            Assert.IsNotNull(gotUser);

            Assert.AreEqual(userId, gotUser.Id);
        }

        [TestMethod]
        public void DeleteBankAccountSuccessfully()
        {
            var repo = new CardAccountRepository();
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
            Assert.IsTrue(createdAccount.Active);
            var result = repo.DeleteCardAccount(createdAccount.Id);

            Assert.IsTrue(result);

            var gotAccount = repo.GetCardAccountById(createdAccount.Id);
            Assert.IsFalse(gotAccount.Active);
        }

    }
}
