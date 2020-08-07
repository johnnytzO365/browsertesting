using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;

namespace NBG.PublicSiteNewApps.CommonLibrary.Log
{
    public static class Logger
    {
        public static void LogEvent(string message, System.Diagnostics.EventLogEntryType type)
        {
            string source = new System.Diagnostics.StackFrame(1).GetMethod().Name;
            LoggingService.LogError(LoggingService.CATEGORY_GENERAL, source, message, type);
            //SPSecurity.RunWithElevatedPrivileges(delegate
            //{
            //    try
            //    {
            //        System.Diagnostics.EventLog.WriteEntry(source, message, type);
            //    }
            //    catch (Exception)
            //    {

            //    }
            //});
        }

        public static void LogEvent(string message)
        {
            LogEvent(message, System.Diagnostics.EventLogEntryType.Information);
        }

        public static void LogEvent(string source, string message)
        {
            LogEvent(source, message, System.Diagnostics.EventLogEntryType.Information);
        }

        public static void LogEvent(string source, string message, System.Diagnostics.EventLogEntryType type)
        {
            LoggingService.LogError(LoggingService.CATEGORY_GENERAL, source, message, type);

            //SPSecurity.RunWithElevatedPrivileges(delegate
            //{
            //    try
            //    {
            //        System.Diagnostics.EventLog.WriteEntry(source, message, type);
            //    }
            //    catch (Exception)
            //    {

            //    }
            //});
        }

        public static string BuildLogString(string itemId, string procedure, string data, string error)
        {
            return string.Format(Core.Configuration.ERROR_STRING_FORMAT, itemId, procedure, data, error);
        }

    }

}

