//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions
{
    public class NotFoundVideoMetadataException : Xeption
    {
        public NotFoundVideoMetadataException(string message)
            :base(message) { } 
    }
}
