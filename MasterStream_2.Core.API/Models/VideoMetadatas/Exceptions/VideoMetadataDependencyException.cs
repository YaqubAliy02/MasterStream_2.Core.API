﻿//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions
{
    public class VideoMetadataDependencyException : Xeption
    {
        public VideoMetadataDependencyException(string message, Xeption innerException)
        : base(message, innerException) { }
    }
}
