using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task ShouldRetrieveVideoMetadataByIdAsync()
        {
            //given
            Guid randomVideoMetadataId = Guid.NewGuid();
            Guid inputVideoMetadataId = randomVideoMetadataId;
            VideoMetadata randomVideoMetadata = CreateRandomVideoMetadata();
            VideoMetadata storageVideoMetadata = randomVideoMetadata;
            VideoMetadata expectedVideoMetadata = storageVideoMetadata.DeepClone();

            this.storageBrokerMock.Setup(broker =>
               broker.SellectVideoMetadataByIdAsync(inputVideoMetadataId))
                   .ReturnsAsync(storageVideoMetadata);

            //when
            VideoMetadata actualVideoMetadata =
                await this.videoMetadataService.RetrieveVideoMetadataByIdAsync(inputVideoMetadataId);

            //then
            actualVideoMetadata.Should().BeEquivalentTo(expectedVideoMetadata);

            this.storageBrokerMock.Verify(broker =>
                broker.SellectVideoMetadataByIdAsync(inputVideoMetadataId), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
