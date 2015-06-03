﻿using PromisePayDotNet.DTO;
using System.Collections.Generic;

namespace PromisePayDotNet.Interfaces
{
    public interface IUploadRepository
    {
        IList<Upload> ListUploads();

        Upload GetUploadById(string uploadId);

        Upload CreateUpload(string csvData);

        Upload GetStatus(string uploadId);

        Upload StartImport(string uploadId);
    }
}
