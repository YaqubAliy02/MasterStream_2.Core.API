//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream_2.Core.API.Models.VideoMetadatas;

namespace MasterStream_2.Core.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<VideoMetadata> InsertVideoMetadata(VideoMetadata videoMetadata);
        IQueryable<VideoMetadata> SellectAllVideoMetadatas();
        ValueTask<VideoMetadata> SellectVideoMetadataByIdAsync(Guid videoMetadataId);
        ValueTask<VideoMetadata> UpdateVideoMetadataAsync(VideoMetadata videoMetadata);
        ValueTask<VideoMetadata> DeleteVideoMetadataAsync(VideoMetadata videoMetadata);
    }
}
