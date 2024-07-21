﻿using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;

namespace MasterStream_2.Core.API.Brokers.Blobs
{
    public partial class BlobBroker
    {
        public async Task<string> UploadVideoAsync(Stream fileStream, string fileName, string contentType) =>
            await UploadAsync(fileStream, fileName, contentType);

        public async Task<Stream> GetBlobStreamAsync(string blobName, string containerName)
        {
            var blobServiceClient = new BlobServiceClient(blobConnectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();

            var memoryStream = new MemoryStream();
            await blobDownloadInfo.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}
