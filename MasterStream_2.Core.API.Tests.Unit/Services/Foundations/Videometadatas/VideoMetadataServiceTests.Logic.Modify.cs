//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using MasterStream_2.Core.API.Models.VideoMetadatas;
using Moq;

namespace MasterStream_2.Core.API.Tests.Unit.Services.Foundations.Videometadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldModifyVideoMetadataAsync()
        {
            //given
            VideoMetadata randomVideoMetadata = CreateRandomVideoMetadata();
            VideoMetadata inputVideoMetadata = randomVideoMetadata;
            VideoMetadata storageVideoMetadata = inputVideoMetadata.DeepClone();
            VideoMetadata updatedVideoMetadata = inputVideoMetadata;
            VideoMetadata expectedVideoMetadata = updatedVideoMetadata.DeepClone();
            Guid videoMetadataId = inputVideoMetadata.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SellectVideoMetadataByIdAsync(videoMetadataId))
                    .ReturnsAsync(storageVideoMetadata);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateVideoMetadataAsync(inputVideoMetadata))
                    .ReturnsAsync(updatedVideoMetadata);

            //when
            VideoMetadata actualVideoMetadata =
                await this.videoMetadataService.ModifyVideoMetadataAsync(inputVideoMetadata);

            //then
            actualVideoMetadata.Should().BeEquivalentTo(expectedVideoMetadata);

            this.storageBrokerMock.Verify(broker =>
                broker.SellectVideoMetadataByIdAsync(videoMetadataId),
                    Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateVideoMetadataAsync(inputVideoMetadata),
                    Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
