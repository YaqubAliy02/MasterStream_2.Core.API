//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions
{
    public class FailedVideoMetadataStorageException : Xeption
    {
        public FailedVideoMetadataStorageException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}
