using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Blob;

namespace StarWarsForum.Data
{
    public interface IUploadService
    {
        CloudBlobContainer GetBlobContainer(string connectionString);
    }
}
