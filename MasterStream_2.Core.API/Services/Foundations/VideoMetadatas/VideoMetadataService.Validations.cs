//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using System.Data;
using MasterStream_2.Core.API.Brokers.DateTimes;
using MasterStream_2.Core.API.Models.VideoMetadatas;
using MasterStream_2.Core.API.Models.VideoMetadatas.Exceptions;

namespace MasterStream_2.Core.API.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataService
    {
        private void ValidateVideoMetadataOnAdd(VideoMetadata videoMetadata)
        {
            ValidateVideoMetadata(videoMetadata);

            Validate(
                (Rule: IsInvalid(videoMetadata.Id), Parameter: nameof(videoMetadata.Id)),
                (Rule: IsInvalid(videoMetadata.Title), Parameter: nameof(videoMetadata.Title)),
                (Rule: IsInvalid(videoMetadata.BlobPath), Parameter: nameof(videoMetadata.BlobPath)),
                (Rule: IsInvalid(videoMetadata.CreatedDate), Parameter: nameof(videoMetadata.CreatedDate)),
                (Rule: IsInvalid(videoMetadata.UpdatedDate), Parameter: nameof(videoMetadata.UpdatedDate)),
                (Rule: IsNotRecent(videoMetadata.CreatedDate), Parameter: nameof(videoMetadata.CreatedDate))
                );
        }

        private void ValidateVideoMetadata(VideoMetadata videoMetadata)
        {
            if (videoMetadata == null)
            {
                throw new NullVideoMetadataException("Video metadata is null");
            }
        }

        public dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsDateNotRecent(date),
            Message = "Date is not recent"
        };

        public bool IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime = this.dateTimeBroker.GetCurrentDateTimeOffset();
            TimeSpan timeDifference = currentDateTime.Subtract(date);

            return timeDifference.TotalSeconds is > 60 or 0;
        }
        private dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidVideoMetadataException =
                new InvalidVideoMetadataException(message: "Video metadata is invalid");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidVideoMetadataException.UpsertDataList(
                        parameter,
                        value: rule.Message);
                }
            }

            invalidVideoMetadataException.ThrowIfContainsErrors();
        }
    }
}
