using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Implementations;
using System;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void TransactionDeserialization()
        {
            var jsonStr = "{\"id\": \"8d8237c2-8598-4100-9fa5-f4ced75e7d76\",\"created_at\": \"2014-12-29T09:40:47.046Z\",\"updated_at\": \"2014-12-29T09:40:47.489Z\",\"description\": \"Buyer Fee @ 10%\",\"amount\": 5000,\"currency\":\"USD\",\"type\":\"debit\",\"from\": \"Escrow Vault\",\"to\": \"Awesome Websites\",\"related\": {\"transactions\":\"6a5525cf-e82f-40e7-995a-ad747185052a\"},\"links\":{\"self\":\"/transactions/8d8237c2-8598-4100-9fa5-f4ced75e7d76\",\"users\":\"/transactions/8d8237c2-8598-4100-9fa5-f4ced75e7d76/users\",\"fees\":\"/transactions/8d8237c2-8598-4100-9fa5-f4ced75e7d76/fees\"}}";
            var transaction = JsonConvert.DeserializeObject<Transaction>(jsonStr);
            Assert.AreEqual("8d8237c2-8598-4100-9fa5-f4ced75e7d76", transaction.Id);
        }

        [TestMethod]
        public void ListTransactionsSuccessful()
        {
            //First, create a user, so we'll have at least one 
            var repo = new TransactionRepository();
            //Then, list users
            var transactions = repo.ListTransactions(200);

            Assert.IsNotNull(transactions);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ListTransactionsNegativeParams()
        {
            var repo = new TransactionRepository();
            repo.ListTransactions(-10, -20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ListTransactionsTooHighLimit()
        {
            var repo = new TransactionRepository();
            repo.ListTransactions(201);
        }
    }
}
