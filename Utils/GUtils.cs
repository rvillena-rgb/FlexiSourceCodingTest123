using System.Diagnostics;

namespace FlexiSourceCodingTest.Utils
{
    public class GUtils
    {
        public void WriteToEventLog(string logName, string logSource, string logMessage, EventLogEntryType eventLogEntryType)
        {
            try
            {
                if (!EventLog.SourceExists(logSource))
                {
                    EventLog.CreateEventSource(logSource, logName);
                }

                EventLog eventLog = new EventLog();
                eventLog.Source = logSource;
                eventLog.WriteEntry(logMessage, eventLogEntryType);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
