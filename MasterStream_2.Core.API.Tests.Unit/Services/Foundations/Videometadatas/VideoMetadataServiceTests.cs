//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using MasterStream_2.Core.API.Brokers.DateTimes;
using MasterStream_2.Core.API.Brokers.Loggings;
using MasterStream_2.Core.API.Brokers.Storages;
using MasterStream_2.Core.API.Models.VideoMetadatas;
using MasterStream_2.Core.API.Services.Foundations.VideoMetadatas;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace MasterStream_2.Core.API.Tests.Unit.Services.Foundations.Videometadatas
{
    public partial class VideoMetadataServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;

        private readonly IVideoMetadataService videoMetadataService;

        public VideoMetadataServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();

            this.videoMetadataService = new VideoMetadataService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object);
        }
        private string GetRandomString() =>
            new MnemonicString().GetValue().ToString();

        private static VideoMetadata CreateRandomVideoMetadata() =>
            CreateRandomVideoMetadataFiller(dates: CreateRandomDateTimeOffset()).Create();
        private static VideoMetadata CreateRandomVideoMetadata(DateTimeOffset date) =>
            CreateRandomVideoMetadataFiller(dates: date).Create();

        private IQueryable<VideoMetadata> CreateRandomVideoMetadatas()
        {
            return CreateRandomVideoMetadataFiller(dates: CreateRandomDateTimeOffset())
                .Create(count: GetRandomNumber()).AsQueryable();
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static DateTimeOffset CreateRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static Filler<VideoMetadata> CreateRandomVideoMetadataFiller(DateTimeOffset dates)
        {
            var filler = new Filler<VideoMetadata>();

            filler.Setup().
                OnType<DateTimeOffset>().Use(dates);

            return filler;
        }
    }
}
