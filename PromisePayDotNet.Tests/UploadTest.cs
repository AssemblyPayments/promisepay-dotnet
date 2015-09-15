using Newtonsoft.Json;
using NUnit.Framework;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Implementations;
using RestSharp;

namespace PromisePayDotNet.Tests
{
    public class UploadTest
    {
        [Test]
        public void UploadDeserialization()
        {
            const string jsonStr = "{ \"id\": \"a2711d90-ed41-4d12-81d2-000000000002\", \"processed_lines\": 6, \"total_lines\": 6, \"update_lines\": 0, \"error_lines\": 6, \"progress\": 100.0 }";
            var upload = JsonConvert.DeserializeObject<Upload>(jsonStr);
            Assert.IsNotNull(upload);
            Assert.AreEqual("a2711d90-ed41-4d12-81d2-000000000002", upload.Id);
        }

        [Test]
        [Ignore]
        public void CreateUploadSuccessfully()
        {
            Assert.Fail();
        }

        [Test]
        [Ignore]
        public void ListUploadsSuccessfully()
        {
            var repo = new UploadRepository(new RestClient());
            var uploads = repo.ListUploads();
            Assert.IsNotNull(uploads);
        }

        [Test]
        [Ignore]
        public void GetUploadByIdSuccessfully()
        {
            Assert.Fail();
        }



        [Test]
        [Ignore]
        public void GetStatusSuccessfully()
        {
            Assert.Fail();            
        }

        [Test]
        [Ignore]
        public void StartImportSuccessfully()
        {
            Assert.Fail();            
        }


    }
}
