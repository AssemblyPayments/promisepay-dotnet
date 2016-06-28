using System.Collections.Generic;

namespace PromisePayDotNet.Dynamic.Interfaces
{
    public interface IUploadRepository
    {
        IDictionary<string,object> ListUploads();

        IDictionary<string, object> GetUploadById(string uploadId);

        IDictionary<string, object> CreateUpload(string csvData);

        IDictionary<string, object> GetStatus(string uploadId);

        IDictionary<string, object> StartImport(string uploadId);
    }
}
