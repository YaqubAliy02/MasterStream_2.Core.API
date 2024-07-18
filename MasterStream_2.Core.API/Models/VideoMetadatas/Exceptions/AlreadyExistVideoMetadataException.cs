//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions
{
    public class AlreadyExistVideoMetadataException : Xeption
    {
        public AlreadyExistVideoMetadataException(string message, Exception innerException)
         : base(message, innerException) { }
    }
}
