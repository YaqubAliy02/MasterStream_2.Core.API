//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using System.Linq;
using FluentAssertions;
using MasterStream_2.Core.API.Models.VideoMetadatas;
using Moq;

namespace MasterStream_2.Core.API.Tests.Unit.Services.Foundations.Videometadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public void ShouldReturnVideoMetadatas()
        {
            //given
            IQueryable<VideoMetadata> randomVideoMetadata = CreateRandomVideoMetadatas();
            IQueryable<VideoMetadata> storageVideoMetadata = randomVideoMetadata;
            IQueryable<VideoMetadata> expectedVideoMetadata = storageVideoMetadata;

            this.storageBrokerMock.Setup(broker =>
                broker.SellectAllVideoMetadatas()).Returns(storageVideoMetadata);

            //when
            IQueryable<VideoMetadata> actualVideoMetadata = 
                this.videoMetadataService.RetrieveAllVideoMetadatas();

            //then
            actualVideoMetadata.Should().BeEquivalentTo(expectedVideoMetadata);

            this.storageBrokerMock.Verify(broker =>
                broker.SellectAllVideoMetadatas(), 
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
