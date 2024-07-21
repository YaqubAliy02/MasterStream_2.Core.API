//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream_2.Core.API.Models.Videos
{
    public class ExceptionsInvalidVideoExceptions : Xeption
    {
        public ExceptionsInvalidVideoExceptions(string message)
         : base(message) { }

    }
}