//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

namespace MasterStream_2.Core.API.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger<LoggingBroker> loggingBroker;

        public LoggingBroker(ILogger<LoggingBroker> loggingBroker)
        {
            this.loggingBroker = loggingBroker;
        }
        public void LogCritical(Exception exception) =>
            this.loggingBroker.LogCritical(exception.Message);

        public void LogError(Exception exception) =>
            this.loggingBroker.LogError(exception.Message);
    }
}
