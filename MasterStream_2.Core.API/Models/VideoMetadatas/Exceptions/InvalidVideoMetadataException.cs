//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions
{
    public class InvalidVideoMetadataException : Xeption
    {
        public InvalidVideoMetadataException(string message)
            :base(message) { }
    }
}
