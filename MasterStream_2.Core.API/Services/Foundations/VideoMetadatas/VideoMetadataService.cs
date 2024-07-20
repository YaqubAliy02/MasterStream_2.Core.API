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
    public partial class VideoMetadataService : IVideoMetadataService
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

        public ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata) =>
            TryCatch(async () =>
            {
                ValidationVideoMetadataOnAdd(videoMetadata);
                return await this.storageBroker.InsertVideoMetadataAsync(videoMetadata);
            });

        public IQueryable<VideoMetadata> RetrieveAllVideoMetadatas() =>
            TryCatch(() =>
            {
                return this.storageBroker.SellectAllVideoMetadatas();
            });

        public ValueTask<VideoMetadata> RetrieveVideoMetadataByIdAsync(Guid videoMetadataId) =>
        TryCatch(async () =>
        {
            ValidateVideoMetadataId(videoMetadataId);

            VideoMetadata mayBeVideoMetadata =
                    await this.storageBroker.SellectVideoMetadataByIdAsync(videoMetadataId);

            ValidationStorageVideoMetadata(mayBeVideoMetadata, videoMetadataId);

            return mayBeVideoMetadata;

        });
        public async ValueTask<VideoMetadata> ModifyVideoMetadataAsync(VideoMetadata videoMetadata)
        {
            VideoMetadata maybeVideoMetadata =
                    await this.storageBroker.SellectVideoMetadataByIdAsync(videoMetadata.Id);

            return await this.storageBroker.UpdateVideoMetadataAsync(videoMetadata);
        }
    }
}
