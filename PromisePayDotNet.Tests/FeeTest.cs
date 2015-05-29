using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PromisePayDotNet.Tests
{
    [TestClass]
    public class FeeTest
    {
        [TestMethod]
        public void TestDeserialization()
        {
            var jsonStr =
                "{ \"id\": \"58e15f18-500e-4cdc-90ca-65e1f1dce565\", \"created_at\": \"2014-12-29T08:31:42.168Z\", \"updated_at\": \"2014-12-29T08:31:42.168Z\", \"name\": \"Buyer Fee @ 10%\", \"fee_type_id\": 2, \"amount\": 1000, \"cap\": null, \"min\": null, \"max\": null, \"to\": \"buyer\", \"links\": { \"self\": \"/fees/58e15f18-500e-4cdc-90ca-65e1f1dce565\" } }";

        }
    }
}
