//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using System;
using FluentAssertions;
using MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;

namespace MasterStream_2.Core.API.Tests.Unit.Services.Foundations.Videometadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]

        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenSqlExceptionOccursAndLogIt()
        {
            //given
            SqlException sqlException = GetSqlException();

            var failedVideoMetadataStorageException =
                new FailedVideoMetadataStorageException(
                    message: "Failed Video Metadata storage error occured, please contact support.",
                    innerException: sqlException);

            var expectedVideoMetadataDependencyException =
                new VideoMetadataDependencyException(
                    message: "Video metadata dependency error occured, fix the errors and try again.",
                    innerException: failedVideoMetadataStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SellectAllVideoMetadatas()).Throws(sqlException);

            //when
            Action retrieveAllVideoMetadatasAction = () =>
                this.videoMetadataService.RetrieveAllVideoMetadatas();

            VideoMetadataDependencyException actualVideoMetadataDependencyException =
                Assert.Throws<VideoMetadataDependencyException>(retrieveAllVideoMetadatasAction);

            //then
            actualVideoMetadataDependencyException.Should()
                .BeEquivalentTo(expectedVideoMetadataDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SellectAllVideoMetadatas(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedVideoMetadataDependencyException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogIt()
        {
            //given
            string exceptionString = GetRandomString();
            var serviceException = new Exception(exceptionString);

            var failedVideoMetadataServiceException =
                new FailedVideoMetadataServiceException(
                    message: "Unexpected error of Video Metadata occured.",
                    innerException: serviceException);

            VideoMetadataServiceException expectedVideoMetadataServiceException =
                new VideoMetadataServiceException(
                    message: "Unexpected service error occured. Contact support.",
                    innerException: failedVideoMetadataServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SellectAllVideoMetadatas()).Throws(serviceException);

            //when
            Action retrieveAllVideoMetadataAction = () =>
                this.videoMetadataService.RetrieveAllVideoMetadatas();

            VideoMetadataServiceException actualVideoMetadataServiceException =
                Assert.Throws<VideoMetadataServiceException>(retrieveAllVideoMetadataAction);

            //then
            actualVideoMetadataServiceException.Should()
                    .BeEquivalentTo(expectedVideoMetadataServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SellectAllVideoMetadatas(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataServiceException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
