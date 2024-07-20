//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using MasterStream_2.Core.API.Models.VideoMetadatas;
using MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions;
using Moq;

namespace MasterStream_2.Core.API.Tests.Unit.Services.Foundations.Videometadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIsInvalidAndLogItAsync()
        {
            //given
            var invalidVideoMetadataId = Guid.Empty;

            var invalidVideoMetadataException =
                new InvalidVideoMetadataException(
                    message: "Video metadata is invalid");

            invalidVideoMetadataException.AddData(
                key: nameof(VideoMetadata.Id),
                values: "Id is required");

            var expectedVideoMetadataValidationException =
                new VideoMetadataValidationException(
                    message: "Video metadata validation the error occured, fix errors and try again.",
                    innerException: invalidVideoMetadataException);

            //when
            ValueTask<VideoMetadata> retrieveByIdVideoMetadataTask =
                this.videoMetadataService.RetrieveVideoMetadataByIdAsync(invalidVideoMetadataId);

            VideoMetadataValidationException actualVideoMetadataValidationException =
                await Assert.ThrowsAsync<VideoMetadataValidationException>(
                    retrieveByIdVideoMetadataTask.AsTask);

            //then
            actualVideoMetadataValidationException.Should().BeEquivalentTo(
                expectedVideoMetadataValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SellectVideoMetadataByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionOnRetrieveByIdIfVideoMetadataIsNotFoundAndLogItAsync()
        {
            //given
            Guid someVideoMetadataId = Guid.NewGuid();
            VideoMetadata noVideoMetadata = null;

            NotFoundVideoMetadataException notFoundVidoeMetadataException =
                new NotFoundVideoMetadataException($"Could not find video metadata with id {someVideoMetadataId}");

            VideoMetadataValidationException expectedVideoMetadataValidationException =
                new VideoMetadataValidationException(
                    message: "Video metadata validation the error occured, fix errors and try again.",
                        notFoundVidoeMetadataException);

            this.storageBrokerMock.Setup(broker =>
                broker.SellectVideoMetadataByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(noVideoMetadata);

            //when
            ValueTask<VideoMetadata> retrieveByIdVideoMetadataTask =
                this.videoMetadataService.RetrieveVideoMetadataByIdAsync(someVideoMetadataId);

            VideoMetadataValidationException actualVideoMetadataValidationException =
                await Assert.ThrowsAsync<VideoMetadataValidationException>(
                    retrieveByIdVideoMetadataTask.AsTask);

            //then
            actualVideoMetadataValidationException.Should().BeEquivalentTo(
                expectedVideoMetadataValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SellectVideoMetadataByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataValidationException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
