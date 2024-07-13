//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream_2.Core.API.Models.VideoMetadatas;
using Microsoft.EntityFrameworkCore;

namespace MasterStream_2.Core.API.Brokers.Storages
{
    public partial class StorageBroker : IStorageBroker
    {
        private DbSet<VideoMetadata> VideoMetadatas { get; set; }

        public async ValueTask<VideoMetadata> InsertVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await this.InsertAsync(videoMetadata);

        public IQueryable<VideoMetadata> SellectAllVideoMetadatas() =>
            this.SellectAll<VideoMetadata>();

        public async ValueTask<VideoMetadata> SellectVideoMetadataByIdAsync(Guid videoMetadataId) =>
            await this.SelectAsync<VideoMetadata>(videoMetadataId);

        public async ValueTask<VideoMetadata> UpdateVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await UpdateAsync(videoMetadata);
        public async ValueTask<VideoMetadata> DeleteVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await this.DeleteAsync(videoMetadata);
    }
}
