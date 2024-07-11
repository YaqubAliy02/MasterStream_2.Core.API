using MasterStream_2.Core.API.Models.VideoMetadatas;
using Microsoft.EntityFrameworkCore;

namespace MasterStream_2.Core.API.Brokers.Storages
{
    public partial class StorageBroker
    {
        private DbSet<VideoMetadata> VideoMetadatas { get; set; }
    }
}
