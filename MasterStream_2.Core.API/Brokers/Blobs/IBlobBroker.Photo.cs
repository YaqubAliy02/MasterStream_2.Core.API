﻿//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

namespace MasterStream_2.Core.API.Brokers.Blobs
{
    public partial interface IBlobBroker
    {
        Task<string> UploadPhotoAsync(Stream fileStream, string fileName, string contentType);
    }
}
