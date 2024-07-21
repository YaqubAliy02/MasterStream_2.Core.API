//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------


using Azure.Storage.Blobs;

namespace MasterStream_2.Core.API.Brokers.Blobs
{
    public partial class BlobBroker : IBlobBroker
    {
        private readonly string blobConnectionString;
        private readonly string photoContainerName;
        private readonly HashSet<string> photoExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jpg", ".jpeg", ".png", ".gif" };

        public BlobBroker(IConfiguration configuration)
        {
            blobConnectionString = configuration["AzureBlobStorage: ConnectionString"];
            photoContainerName = configuration["AzureBlobStorage: PhotoContainerName"];
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            var blobContainerName = GetContainerName(extension);

            if (blobContainerName is null)
            {
                throw new InvalidOperationException("Unsupported file type.");
            }

            var blobServiceClient = new BlobServiceClient(blobConnectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(fileStream, true);

            return blobClient.Uri.ToString();

        }

        private string GetContainerName(string extension)
        {
            if (photoExtensions.Contains(extension))
            {
                return photoContainerName;
            }

            return null;
        }
    }
}
