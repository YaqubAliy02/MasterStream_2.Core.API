//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream_2.Core.API.Brokers.DateTimes;
using MasterStream_2.Core.API.Brokers.Loggings;
using MasterStream_2.Core.API.Brokers.Storages;
using MasterStream_2.Core.API.Models.VideoMetadatas;

namespace MasterStream_2.Core.API.Services.Foundations.VideoMetadatas
{
    public class VideoMetadataService : IVideoMetadataService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public VideoMetadataService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await this.storageBroker.InsertVideoMetadataAsync(videoMetadata);
    }
}
