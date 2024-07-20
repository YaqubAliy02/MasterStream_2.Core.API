//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream_2.Core.API.Models.Photos.Exceptions
{
    public class InvalidPhotoException : Xeption
    {
        public InvalidPhotoException(string message) 
            : base(message) { }
    }
}
