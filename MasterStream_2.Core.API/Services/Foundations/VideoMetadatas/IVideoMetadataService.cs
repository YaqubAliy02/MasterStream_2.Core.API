﻿//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------


//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream_2.Core.API.Models.VideoMetadatas;

namespace MasterStream_2.Core.API.Services.Foundations.VideoMetadatas
{
    public interface IVideoMetadataService
    {
        ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata);
    }
}