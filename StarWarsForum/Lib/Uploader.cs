using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using StarWarsForum.Data;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StarWarsForum.Lib
{
    public class Uploader
    {
        public static async Task<Uri> UploadImage(string blobContainer, IFormFile file, IConfiguration _configuration, IUploadService _uploadService)
        {
            var connectionString = _configuration.GetConnectionString("AzureStorageAccount");

            var container = _uploadService.GetBlobContainer(connectionString, blobContainer);

            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            var filename = contentDisposition.FileName.Trim('"');

            var blockBlob = container.GetBlockBlobReference(filename);

            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            return blockBlob.Uri;
        }
    }
}
