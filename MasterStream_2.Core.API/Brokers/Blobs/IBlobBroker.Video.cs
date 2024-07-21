namespace MasterStream_2.Core.API.Brokers.Blobs
{
    public partial interface IBlobBroker
    {
        Task<string> UploadVideoAsync(Stream fileSteam, string fileName, string contentType);
        Task<Stream> GetBlobStreamAsync(string blobName, string containerName);
    }
}
