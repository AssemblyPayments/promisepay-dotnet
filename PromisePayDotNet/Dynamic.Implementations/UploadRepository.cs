using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class UploadRepository : PromisePayDotNet.Implementations.AbstractRepository,
                                    PromisePayDotNet.Dynamic.Interfaces.IUploadRepository
    {
        public UploadRepository(IRestClient client)
            : base(client)
        {
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IDictionary<string,object> ListUploads()
        {
            var request = new RestRequest("/uploads", Method.GET);

            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }

        public IDictionary<string, object> GetUploadById(string uploadId)
        {
            AssertIdNotNull(uploadId);
            var request = new RestRequest("/uploads/{id}", Method.GET);
            request.AddUrlSegment("id", uploadId);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }

        public IDictionary<string, object> CreateUpload(string csvData)
        {
            if (String.IsNullOrEmpty(csvData))
            {
                throw new ArgumentException("csvData cannot be empty");
            }

            var request = new RestRequest("/uploads", Method.POST);
            request.AddParameter("import", csvData);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }

        public IDictionary<string, object> GetStatus(string uploadId)
        {
            AssertIdNotNull(uploadId);
            var request = new RestRequest("/uploads/{id}/import", Method.GET);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }

        public IDictionary<string, object> StartImport(string uploadId)
        {
            AssertIdNotNull(uploadId);
            var request = new RestRequest("/uploads/{id}/import", Method.PATCH);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }
    }
}
