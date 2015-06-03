using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Enums;
using PromisePayDotNet.Exceptions;
using PromisePayDotNet.Implementations;
using System;
using System.Linq;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    public class FeeTest
    {
        [TestMethod]
        public void FeeDeserialization()
        {
            var jsonStr =
                "{ \"id\": \"58e15f18-500e-4cdc-90ca-65e1f1dce565\", \"created_at\": \"2014-12-29T08:31:42.168Z\", \"updated_at\": \"2014-12-29T08:31:42.168Z\", \"name\": \"Buyer Fee @ 10%\", \"fee_type_id\": 2, \"amount\": 1000, \"cap\": null, \"min\": null, \"max\": null, \"to\": \"buyer\", \"links\": { \"self\": \"/fees/58e15f18-500e-4cdc-90ca-65e1f1dce565\" } }";

            var fee = JsonConvert.DeserializeObject<Fee>(jsonStr);
            Assert.IsNotNull(fee);
            Assert.AreEqual("58e15f18-500e-4cdc-90ca-65e1f1dce565", fee.Id);
            Assert.AreEqual("Buyer Fee @ 10%", fee.Name);

        }

        [TestMethod]
        public void CreateFeeSuccessfully()
        {
            var repo = new FeeRepository();
            var feeId = Guid.NewGuid().ToString();
            var createdFee = repo.CreateFee(new Fee
            {
                Id = feeId,
                Amount = 1000,
                Name = "Test fee #1",
                FeeType = FeeType.Fixed,
                Cap = "1",
                Max = "3",
                Min = "2",
                To = "buyer"
            });
            Assert.IsNotNull(createdFee);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateFeeWrongTo()
        {
            var repo = new FeeRepository();
            var feeId = Guid.NewGuid().ToString();
            var createdFee = repo.CreateFee(new Fee
            {
                Id = feeId,
                Amount = 1000,
                Name = "Test fee #1",
                FeeType = FeeType.Fixed,
                Cap = "1",
                Max = "3",
                Min = "2",
            });
            Assert.IsNotNull(createdFee);
            Assert.AreEqual("Test fee #1", createdFee.Name);
        }

        [TestMethod]
        public void GetFeeByIdSuccessfull()
        {
            var repo = new FeeRepository();
            var id = "79116c9f-d750-4faa-85c7-b7da36f23b38";
            var fee = repo.GetFeeById(id);
            Assert.AreEqual(id, fee.Id);
        }

        [TestMethod]
        public void ListFeeSuccessfully()
        {
            var repo = new FeeRepository();
            var fees = repo.ListFees();
            Assert.IsNotNull(fees);
            Assert.IsTrue(fees.Any());
        }
    }
}
