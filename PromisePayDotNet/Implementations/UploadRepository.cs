using System.Collections.Generic;
using PromisePayDotNet.DAO;
using PromisePayDotNet.Interfaces;

namespace PromisePayDotNet.Implementations
{
    public class UploadRepository : AbstractRepository, IUploadRepository
    {
        public IList<Upload> ListUploads()
        {
            throw new System.NotImplementedException();
        }

        public Upload GetUploadById(string uploadId)
        {
            throw new System.NotImplementedException();
        }

        public Upload CreateUpload(string csvFile)
        {
            throw new System.NotImplementedException();
        }

        public Upload GetStatus(string uploadId)
        {
            throw new System.NotImplementedException();
        }

        public Upload StartImport(string uploadId)
        {
            throw new System.NotImplementedException();
        }
    }
}
