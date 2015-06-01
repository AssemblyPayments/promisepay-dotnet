using PromisePayDotNet.DAO;
using System.Collections.Generic;

namespace PromisePayDotNet.Interfaces
{
    public interface IUploadRepository
    {
        IList<Upload> ListUploads();

        Upload GetUploadById(string uploadId);

        Upload CreateUpload(string csvFile);

        Upload GetStatus(string uploadId);

        Upload StartImport(string uploadId);
    }
}
