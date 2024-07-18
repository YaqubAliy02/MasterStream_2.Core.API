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

        public async Task ShouldAddVideoMetadataAsync()
        {
            //given 
            DateTimeOffset randomDateTimeOffset = CreateRandomDateTimeOffset();
            VideoMetadata randomVideoMetadata = CreateRandomVideoMetadata(randomDateTimeOffset);
            VideoMetadata inputVideoMetadata = randomVideoMetadata;
            VideoMetadata storageVideoMetadata = inputVideoMetadata;
            VideoMetadata expectedVideoMetadata = storageVideoMetadata.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTimeOffset);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertVideoMetadataAsync(inputVideoMetadata)).
                    ReturnsAsync(storageVideoMetadata);

            //when
            VideoMetadata actualVideoMetadata =
            await this.videoMetadataService.AddVideoMetadataAsync(inputVideoMetadata);

            //then
            actualVideoMetadata.Should().BeEquivalentTo(expectedVideoMetadata);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertVideoMetadataAsync(inputVideoMetadata),
                    Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
