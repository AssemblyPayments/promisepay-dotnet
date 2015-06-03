using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Implementations;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    public class PayPalAccountTest
    {
        [TestMethod]
        public void PayPalAccountDeserialization()
        {
            var jsonStr = "{ \"active\": true, \"created_at\": \"2015-04-25T12:31:39.324Z\", \"updated_at\": \"2015-04-25T12:31:39.324Z\", \"id\": \"70d93fe3-6c2e-4a1c-918f-13b8e7bb3779\", \"currency\": \"USD\", \"paypal\": { \"email\": \"test.me@promisepay.com\" }, \"links\": { \"self\": \"/paypal_accounts/70d93fe3-6c2e-4a1c-918f-13b8e7bb3779\", \"users\": \"/paypal_accounts/70d93fe3-6c2e-4a1c-918f-13b8e7bb3779/users\" } }";
            var payPalAccount = JsonConvert.DeserializeObject<PayPalAccount>(jsonStr);
            Assert.AreEqual("70d93fe3-6c2e-4a1c-918f-13b8e7bb3779", payPalAccount.Id);
            Assert.AreEqual("USD", payPalAccount.Currency);
            Assert.AreEqual("test.me@promisepay.com", payPalAccount.PayPal.Email);
        }

        [TestMethod]
        public void CreatePayPalAccountSuccessfully()
        {
            var repo = new PayPalAccountRepository();
            var userId = "ec9bf096-c505-4bef-87f6-18822b9dbf2c"; //some user created before
            var account = new PayPalAccount
            {
                UserId = userId,
                Active = true,
                PayPal = new PayPal
                {
                    Email = "aaa@bbb.com"
                }
            };
            var createdAccount = repo.CreatePayPalAccount(account);
            Assert.IsNotNull(createdAccount);
            Assert.IsNotNull(createdAccount.Id);
            Assert.AreEqual("AUD", createdAccount.Currency); // It seems that currency is determined by country
            Assert.IsNotNull(createdAccount.CreatedAt);
            Assert.IsNotNull(createdAccount.UpdatedAt);

        }

        [TestMethod]
        public void GetPayPalAccountSuccessfully()
        {
            var repo = new PayPalAccountRepository();
            var userId = "ec9bf096-c505-4bef-87f6-18822b9dbf2c"; //some user created before
            var account = new PayPalAccount
            {
                UserId = userId,
                Active = true,
                PayPal = new PayPal
                {
                    Email = "aaa@bbb.com"
                }
            };
            var createdAccount = repo.CreatePayPalAccount(account);

            var gotAccount = repo.GetPayPalAccountById(createdAccount.Id);

            Assert.AreEqual(createdAccount.Id, gotAccount.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPayPalAccountEmptyId()
        {
            var repo = new PayPalAccountRepository();
            repo.GetPayPalAccountById(string.Empty);
        }

        [TestMethod]
        public void GetUserForPayPalAccountSuccessfully()
        {
            var repo = new PayPalAccountRepository();
            var userId = "ec9bf096-c505-4bef-87f6-18822b9dbf2c"; //some user created before
            var account = new PayPalAccount
            {
                UserId = userId,
                Active = true,
                PayPal = new PayPal
                {
                    Email = "aaa@bbb.com"
                }
            };
            var createdAccount = repo.CreatePayPalAccount(account);

            var gotUser = repo.GetUserForPayPalAccount(createdAccount.Id);

            Assert.IsNotNull(gotUser);

            Assert.AreEqual(userId, gotUser.Id);
        }

        [TestMethod]
        public void DeletePayPalAccountSuccessfully()
        {
            var repo = new PayPalAccountRepository();
            var userId = "ec9bf096-c505-4bef-87f6-18822b9dbf2c"; //some user created before
            var account = new PayPalAccount
            {
                UserId = userId,
                Active = true,
                PayPal = new PayPal
                {
                    Email = "aaa@bbb.com"
                }
            };
            var createdAccount = repo.CreatePayPalAccount(account);
            Assert.IsTrue(createdAccount.Active);
            var result = repo.DeletePayPalAccount(createdAccount.Id);

            Assert.IsTrue(result);

            var gotAccount = repo.GetPayPalAccountById(createdAccount.Id);
            Assert.IsFalse(gotAccount.Active);
        }

    }
}
