//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using System.Threading.Tasks;
using FluentAssertions;
using MasterStream_2.Core.API.Models.VideoMetadatas;
using MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;

namespace MasterStream_2.Core.API.Tests.Unit.Services.Foundations.Videometadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync() 
        { 
            //given
            VideoMetadata someVideoMetadata = CreateRandomVideoMetadata();
            SqlException sqlException = GetSqlException();

            var failedVideoMetadataStorageException =
                new FailedVideoMetadataStorageException(
                    message: "Failed video metadata error occured, contact support.",
                    innerException: sqlException);

            var expectedVideoMetadataDependencyException =
                new VideoMetadataDependencyException(
                    message: "Video metadata dependency error occured, fix the errors and try again.",
                    innerException: failedVideoMetadataStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertVideoMetadataAsync(someVideoMetadata))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<VideoMetadata> addVideoMetadata = 
                this.videoMetadataService.AddVideoMetadataAsync(someVideoMetadata);

            VideoMetadataDependencyException actualVideoMetadataDependencyException =
                await Assert.ThrowsAsync<VideoMetadataDependencyException>(addVideoMetadata.AsTask);

            //then
            actualVideoMetadataDependencyException.Should()
                .BeEquivalentTo(expectedVideoMetadataDependencyException);

            this.storageBrokerMock.Verify(broker => 
                broker.InsertVideoMetadataAsync(someVideoMetadata),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker => 
                broker.LogCritical(It.Is(SameExceptionAs(
                       expectedVideoMetadataDependencyException))),
                            Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
