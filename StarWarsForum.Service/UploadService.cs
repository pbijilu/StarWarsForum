using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using StarWarsForum.Data;
using System;

namespace StarWarsForum.Services
{
    public class UploadService : IUploadService
    {
        public CloudBlobContainer GetBlobContainer(string connectionString, string blobContainer)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();

            return blobClient.GetContainerReference(blobContainer);
        }
    }
}
