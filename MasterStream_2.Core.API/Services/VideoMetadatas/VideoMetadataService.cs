//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream_2.Core.API.Brokers.DateTimes;
using MasterStream_2.Core.API.Brokers.Loggings;
using MasterStream_2.Core.API.Brokers.Storages;
using MasterStream_2.Core.API.Models.VideoMetadatas;

namespace MasterStream_2.Core.API.Services.VideoMetadatas
{
    public class VideoMetadataService : IVideoMetadataService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public VideoMetadataService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata)
        {
            throw new NotImplementedException();
        }
    }
}
