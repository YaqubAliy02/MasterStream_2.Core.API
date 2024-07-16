//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream_2.Core.API.Models.VideoMetadatas;
using MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions;
using Microsoft.Data.SqlClient;
using STX.EFxceptions.Abstractions.Models.Exceptions;
using Xeptions;

namespace MasterStream_2.Core.API.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataService
    {
        private delegate ValueTask<VideoMetadata> ReturningVideoMetadataFunction();

        private async ValueTask<VideoMetadata> TryCatch(
            ReturningVideoMetadataFunction returningVideoMetadataFunction)
        {
            try
            {
                return await returningVideoMetadataFunction();
            }
            catch (NullVideoMetadataException nullVideoMetadataException)
            {
                throw CreateAndLogValidationException(nullVideoMetadataException);
            }
            catch (InvalidVideoMetadataException invalidVideoMetadataException)
            {
                throw CreateAndLogValidationException(invalidVideoMetadataException);
            }
            catch (SqlException sqlException)
            {
                var failedVideoMetadataStorageException =
                    new FailedVideoMetadataStorageException(
                        message: "Failed video metadata error occured, contact support.",
                        innerException: sqlException);

                throw CreateAndLogCriticalDependencyException(failedVideoMetadataStorageException);
            }
            catch(DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistVideoMetadataException =
                    new AlreadyExistVideoMetadataException(
                        message: "Video metadata already exist, please try again.",
                        innerException: duplicateKeyException);

                throw CreateAndLogDuplicateKeyException(alreadyExistVideoMetadataException);
            }
        }

        private VideoMetadataDependencyValidationException CreateAndLogDuplicateKeyException(Xeption exception)
        {
            var videoMetadataDependencyValidationException =
                new VideoMetadataDependencyValidationException(
                    message: "Video metadata dependency error occured, fix the errors and try again.",
                    innerException: exception);

            this.loggingBroker.LogError(videoMetadataDependencyValidationException);

            return videoMetadataDependencyValidationException;
        }

        private VideoMetadataDependencyException CreateAndLogCriticalDependencyException(FailedVideoMetadataStorageException failedVideoMetadataStorageException)
        {
            var videoMetadataDependencyException =
                new VideoMetadataDependencyException(
                    message: "Video metadata dependency error occured, fix the errors and try again.",
                    innerException: failedVideoMetadataStorageException);

            this.loggingBroker.LogCritical(videoMetadataDependencyException);

            return videoMetadataDependencyException;
        }

        private Exception CreateAndLogValidationException(Xeption exception)
        {
            var videoMetadataValidationException =
                 new VideoMetadataValidationException(
                      message: "Video metadata validation the error occured, fix errors and try again.",
                    innerException: exception);

            this.loggingBroker.LogError(videoMetadataValidationException);

            return videoMetadataValidationException;
        }
    }
}
