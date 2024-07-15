//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions
{
    public class NullVideoMetadataException : Xeption
    {
        public NullVideoMetadataException()
            : base("VideoMetadata is null") { }
    }
}
