//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

namespace MasterStream_2.Core.API.Models.Photos
{
    public class Photo
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public string BlobUri { get; set; }
        public DateTimeOffset UploadedDate { get; set; }
    }
}
