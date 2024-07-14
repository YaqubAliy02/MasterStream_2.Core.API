using Xeptions;

namespace MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions
{
    public class VideoMetadataValidationException : Xeption
    {
        public VideoMetadataValidationException(Exception innerException)
        : base("VideoMetadata is invalid. Please fix the errors and try again.") { }
    }
}
