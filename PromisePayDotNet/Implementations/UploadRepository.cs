using Newtonsoft.Json;
using PromisePayDotNet.DTO;
using PromisePayDotNet.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromisePayDotNet.Implementations
{
    public class UploadRepository : AbstractRepository, IUploadRepository
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IList<Upload> ListUploads()
        {
            var client = GetRestClient();
            var request = new RestRequest("/uploads", Method.GET);

            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("uploads"))
            {
                var uploadCollection = dict["uploads"];
                return JsonConvert.DeserializeObject<List<Upload>>(JsonConvert.SerializeObject(uploadCollection));
            }
            return new List<Upload>();
        }

        public Upload GetUploadById(string uploadId)
        {
            AssertIdNotNull(uploadId);
            var client = GetRestClient();
            var request = new RestRequest("/uploads/{id}", Method.GET);
            request.AddUrlSegment("id", uploadId);
            var response = SendRequest(client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, Upload>>(response.Content).Values.First();
        }

        public Upload CreateUpload(string csvData)
        {
            if (String.IsNullOrEmpty(csvData))
            {
                throw new ArgumentException("csvData cannot be empty");
            }

            var client = GetRestClient();
            var request = new RestRequest("/uploads", Method.POST);
            request.AddParameter("import", csvData);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("uploads"))
            {
                var itemCollection = dict["uploads"];
                return JsonConvert.DeserializeObject<Upload>(JsonConvert.SerializeObject(itemCollection));
            }
            return null;
        }

        public Upload GetStatus(string uploadId)
        {
            AssertIdNotNull(uploadId);
            var client = GetRestClient();
            var request = new RestRequest("/uploads/{id}/import", Method.GET);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("uploads"))
            {
                var itemCollection = dict["uploads"];
                return JsonConvert.DeserializeObject<Upload>(JsonConvert.SerializeObject(itemCollection));
            }
            return null;
        }

        public Upload StartImport(string uploadId)
        {
            AssertIdNotNull(uploadId);
            var client = GetRestClient();
            var request = new RestRequest("/uploads/{id}/import", Method.PATCH);
            var response = SendRequest(client, request);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
            if (dict.ContainsKey("uploads"))
            {
                var itemCollection = dict["uploads"];
                return JsonConvert.DeserializeObject<Upload>(JsonConvert.SerializeObject(itemCollection));
            }
            return null;            
        }
    }
}
