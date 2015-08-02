using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromisePayDotNet.Implementations;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    [Ignore]
    public class ItemActionsTest
    {
        [TestMethod]
        public void MakePaymentSuccessfully()
        {
            var repo = new ItemRepository();
            var itemId = "7c269f52-2236-4aa5-899e-a2e3ecadbc3f";
            var cardId = "2e2ed4fb-4eb2-458e-99f9-2bf0b3004933";
            repo.MakePayment(itemId, cardId);
        }

        [TestMethod]
        public void RequestPaymentSuccessfully()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ReleasePaymentSuccessfully()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void RequestReleaseSuccessfully()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CancelSuccessfully()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void AcknowledgeWireSuccessfully()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void AcknowledgePayPalSuccessfully()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void RevertWireSuccessfully()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void RequestRefundSuccessfully()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void RefundSuccessfully()
        {
            Assert.Fail();
        }
    }
}
