using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Implementations;
using System;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    public class BankAccountTest
    {
        [TestMethod]
        public void TestDeserialization()
        {
            var jsonStr =
                "{ \"active\": true, \"created_at\": \"2015-04-26T06:24:19.248Z\", \"updated_at\": \"2015-04-26T06:24:19.248Z\", \"id\": \"8d65c86c-14f4-4abf-a979-eba0a87b283a\", \"currency\": \"USD\", \"bank\": { \"bank_name\": \"Test Me\", \"country\": \"AUS\", \"account_name\": \"Test Account\", \"routing_number\": \"XXXXXXX3\", \"account_number\": \"XXXX344\", \"holder_type\": \"personal\", \"account_type\": \"savings\" }, \"links\": { \"self\": \"/bank_accounts/8d65c86c-14f4-4abf-a979-eba0a87b283a\", \"users\": \"/bank_accounts/8d65c86c-14f4-4abf-a979-eba0a87b283a/users\" } }";
            var bankAccount = JsonConvert.DeserializeObject<BankAccount>(jsonStr);
            Assert.AreEqual("8d65c86c-14f4-4abf-a979-eba0a87b283a", bankAccount.Id);
            Assert.AreEqual("USD", bankAccount.Currency);
        }

        [TestMethod]
        public void CreateBankAccountSuccessfully()
        {
            var repo = new BankAccountRepository();
            var userId = "ec9bf096-c505-4bef-87f6-18822b9dbf2c"; //some user created before
            var account = new BankAccount
            {
                UserId = userId,
                Active = true,
                Bank = new Bank
                {
                    BankName = "Test bank, inc",
                    AccountName = "Test account",
                    AccountNumber = "8123456789",
                    AccountType = "savings",
                    Country = "AUS",
                    HolderType = "personal",
                    RoutingNumber = "123456"
                }
            };
            var createdAccount = repo.CreateBankAccount(account);
            Assert.IsNotNull(createdAccount);
            Assert.IsNotNull(createdAccount.Id);
            Assert.AreEqual("AUD", createdAccount.Currency); // It seems that currency is determined by country
            Assert.IsNotNull(createdAccount.CreatedAt);
            Assert.IsNotNull(createdAccount.UpdatedAt);
            Assert.AreEqual("XXXXXXX789", createdAccount.Bank.AccountNumber); //Account number is masked
        }

        [TestMethod]
        public void GetBankAccountSuccessfully()
        {
            var repo = new BankAccountRepository();
            var userId = "ec9bf096-c505-4bef-87f6-18822b9dbf2c"; //some user created before
            var account = new BankAccount
            {
                UserId = userId,
                Active = true,
                Bank = new Bank
                {
                    BankName = "Test bank, inc",
                    AccountName = "Test account",
                    AccountNumber = "8123456789",
                    AccountType = "savings",
                    Country = "AUS",
                    HolderType = "personal",
                    RoutingNumber = "123456"
                }
            };
            var createdAccount = repo.CreateBankAccount(account);

            var gotAccount = repo.GetBankAccountById(createdAccount.Id);

            Assert.AreEqual(createdAccount.Id, gotAccount.Id);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void GetBankAccountEmptyId()
        {
            var repo = new BankAccountRepository();
            repo.GetBankAccountById(string.Empty);
        }

        [TestMethod]
        public void GetUserForBankAccountSuccessfully()
        {
            var repo = new BankAccountRepository();
            var userId = "ec9bf096-c505-4bef-87f6-18822b9dbf2c"; //some user created before
            var account = new BankAccount
            {
                UserId = userId,
                Active = true,
                Bank = new Bank
                {
                    BankName = "Test bank, inc",
                    AccountName = "Test account",
                    AccountNumber = "8123456789",
                    AccountType = "savings",
                    Country = "AUS",
                    HolderType = "personal",
                    RoutingNumber = "123456"
                }
            };
            var createdAccount = repo.CreateBankAccount(account);

            var gotUser = repo.GetUserForBankAccount(createdAccount.Id);

            Assert.IsNotNull(gotUser);

            Assert.AreEqual(userId, gotUser.Id);
        }

        [TestMethod]
        public void DeleteBankAccountSuccessfully()
        {
            var repo = new BankAccountRepository();
            var userId = "ec9bf096-c505-4bef-87f6-18822b9dbf2c"; //some user created before
            var account = new BankAccount
            {
                UserId = userId,
                Active = true,
                Bank = new Bank
                {
                    BankName = "Test bank, inc",
                    AccountName = "Test account",
                    AccountNumber = "8123456789",
                    AccountType = "savings",
                    Country = "AUS",
                    HolderType = "personal",
                    RoutingNumber = "123456"
                }
            };
            var createdAccount = repo.CreateBankAccount(account);
            Assert.IsTrue(createdAccount.Active);
            var result = repo.DeleteBankAccount(createdAccount.Id);

            Assert.IsTrue(result);

            var gotAccount = repo.GetBankAccountById(createdAccount.Id);
            Assert.IsFalse(gotAccount.Active);
        }
    }
}
