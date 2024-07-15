//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions
{
    public class VideoMetadataValidationException : Xeption
    {
        public VideoMetadataValidationException( string message, Exception innerException)
        : base(message, innerException) { }
    }
}
