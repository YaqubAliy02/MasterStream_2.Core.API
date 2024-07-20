﻿//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions;
using MasterStream_2.Core.API.Models.VideoMetadatas;
using Moq;
using System.Threading.Tasks;
using System;
using FluentAssertions;

namespace MasterStream_2.Core.API.Tests.Unit.Services.Foundations.Videometadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid videoMetadataId = Guid.Empty;

            var invalidVideoMetadataException =
                new InvalidVideoMetadataException("Video metadata is invalid");

            invalidVideoMetadataException.AddData(
                key: nameof(VideoMetadata.Id),
                values: "Id is required");

            var expectedVideoMetadataValidationException =
                new VideoMetadataValidationException(
                    message: "Video metadata validation error occured, fix errors and try again",
                    innerException: invalidVideoMetadataException);

            // when
            ValueTask<VideoMetadata> removeVideoMetadataTask =
                this.videoMetadataService.RemoveVideoMetadataByIdAsync(videoMetadataId);

            VideoMetadataValidationException actualVideoMetadataValidationException =
                await Assert.ThrowsAsync<VideoMetadataValidationException>(
                    removeVideoMetadataTask.AsTask);

            // then
            actualVideoMetadataValidationException.Should().BeEquivalentTo(
                expectedVideoMetadataValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedVideoMetadataValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SellectVideoMetadataByIdAsync(videoMetadataId),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteVideoMetadataAsync(It.IsAny<VideoMetadata>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
