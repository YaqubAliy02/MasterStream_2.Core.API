//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions;
using MasterStream_2.Core.API.Models.VideoMetadatas;
using Moq;
using System.Threading.Tasks;
using FluentAssertions;

namespace MasterStream_2.Core.API.Tests.Unit.Services.Foundations.Videometadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfVideoMetadataIsNullAndLogItAsync()
        {
            // given
            VideoMetadata nullVideoMetadata = null;
            var nullVideoMetadataException = new NullVideoMetadataException("Video metadata is null");

            var expectedVideoMetadataValidationException =
                new VideoMetadataValidationException(
                    message: "Video metadata validation the error occured, fix errors and try again.",
                    innerException: nullVideoMetadataException);

            // when
            ValueTask<VideoMetadata> modifyVideoMetadataTask =
                this.videoMetadataService.ModifyVideoMetadataAsync(nullVideoMetadata);

            VideoMetadataValidationException actualVideoMetadataValidationException =
                await Assert.ThrowsAsync<VideoMetadataValidationException>(
                    modifyVideoMetadataTask.AsTask);

            // then
            actualVideoMetadataValidationException.Should().BeEquivalentTo(
                expectedVideoMetadataValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataValidationException))),
                    Times.Once());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnModifyIfVideoMetadataIsInvalidAndLogItAsync(
         string invalidText)
        {
            // given
            VideoMetadata invalidVideoMetadata = new VideoMetadata
            {
                Title = invalidText
            };

            var invalidVideoMetadataException =
                new InvalidVideoMetadataException(message: "Video metadata is invalid");

            invalidVideoMetadataException.AddData(
                key: nameof(VideoMetadata.Id),
                values: "Id is required");

            invalidVideoMetadataException.AddData(
                key: nameof(VideoMetadata.Title),
                values: "Text is required");

            invalidVideoMetadataException.AddData(
                key: nameof(VideoMetadata.BlobPath),
                values: "Text is required");

            invalidVideoMetadataException.AddData(
                key: nameof(VideoMetadata.CreatedDate),
                values: "Date is required");

            invalidVideoMetadataException.AddData(
                key: nameof(VideoMetadata.UpdatedDate),
                values: "Date is required");

            var expectedVideoMetadataValidationException =
                new VideoMetadataValidationException(
                    message: "Video metadata validation the error occured, fix errors and try again.",
                    innerException: invalidVideoMetadataException);

            // when
            ValueTask<VideoMetadata> modifyVideoMetadataTask =
                this.videoMetadataService.ModifyVideoMetadataAsync(invalidVideoMetadata);

            VideoMetadataValidationException actualVideoMetadataValidationException =
                await Assert.ThrowsAsync<VideoMetadataValidationException>(
                    modifyVideoMetadataTask.AsTask);

            // then
            actualVideoMetadataValidationException.Should().BeEquivalentTo(
                expectedVideoMetadataValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataValidationException))),
                    Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateVideoMetadataAsync(It.IsAny<VideoMetadata>()),
                    Times.Never());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfVideoMetadataDoesNotExistAndLogItAsync()
        {
            // given
            VideoMetadata randomVideoMetadata = CreateRandomVideoMetadata();
            VideoMetadata nonExistVideoMetadata = randomVideoMetadata;
            VideoMetadata nullVideoMetadata = null;

            var notFoundVideoMetadataException =
                new NotFoundVideoMetadataException(
                    message: $"Could not find video metadata with id {nonExistVideoMetadata.Id}");

            var expectedVideoMetadataValidationException =
                new VideoMetadataValidationException(
                    message: "Video metadata validation the error occured, fix errors and try again.",
                    innerException: notFoundVideoMetadataException);

            this.storageBrokerMock.Setup(broker =>
                broker.SellectVideoMetadataByIdAsync(nonExistVideoMetadata.Id))
                    .ReturnsAsync(nullVideoMetadata);

            // when
            ValueTask<VideoMetadata> modifyVideoMetadataTask =
                this.videoMetadataService.ModifyVideoMetadataAsync(nonExistVideoMetadata);

            VideoMetadataValidationException actualVideoMetadataValidationException =
                await Assert.ThrowsAsync<VideoMetadataValidationException>(
                    modifyVideoMetadataTask.AsTask);

            // then
            actualVideoMetadataValidationException.Should().BeEquivalentTo(
                expectedVideoMetadataValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SellectVideoMetadataByIdAsync(nonExistVideoMetadata.Id),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataValidationException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
