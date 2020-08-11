using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Administration;

namespace NBG.PublicSiteNewApps.CommonLibrary.Log
{
    public class LoggingService : SPDiagnosticsServiceBase
    {
        public static string AREA_NAME = "Custom NBGSite";
        public static string CATEGORY_WORKFLOWINFRASTRUCTURE = "Workflow Infrastructure";
        public static string CATEGORY_PAGES = "Pages";
        public static string CATEGORY_WEBCONTROLS = "Web Controls";
        public static string CATEGORY_GENERAL = "General";

        private static LoggingService _Current;
        public static LoggingService Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new LoggingService();
                }
                return _Current;
            }
        }

        private LoggingService()
            : base(AREA_NAME + " Logging Service", SPFarm.Local)
        {
        }

        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            List<SPDiagnosticsArea> areas = new List<SPDiagnosticsArea>
            {
            new SPDiagnosticsArea(AREA_NAME, new List<SPDiagnosticsCategory>
                {
                new SPDiagnosticsCategory("Workflow Infrastructure", TraceSeverity.Unexpected, EventSeverity.Error),
                new SPDiagnosticsCategory("Web Controls", TraceSeverity.Unexpected, EventSeverity.Error),
                new SPDiagnosticsCategory("Pages", TraceSeverity.Unexpected, EventSeverity.Error),
                new SPDiagnosticsCategory("General", TraceSeverity.Verbose, EventSeverity.Error)
                })
            };
            return areas;
        }

        public static void LogError(string categoryName, string message)
        {
            SPDiagnosticsCategory category = LoggingService.Current.Areas[AREA_NAME].Categories[categoryName];
            LoggingService.Current.WriteTrace(0, category, TraceSeverity.Unexpected, message);
        }

        public static void LogError(string categoryName, string source, string message, System.Diagnostics.EventLogEntryType type)
        {
            SPDiagnosticsCategory category = LoggingService.Current.Areas[AREA_NAME].Categories[categoryName];
            string format = "Source: {0} - Type: {2} - Message: {1}";
            TraceSeverity severity = TraceSeverity.Verbose;
            switch (type)
            {
                case System.Diagnostics.EventLogEntryType.Error:
                    severity = TraceSeverity.Unexpected;
                    break;
                case System.Diagnostics.EventLogEntryType.FailureAudit:
                    severity = TraceSeverity.Unexpected;
                    break;
                case System.Diagnostics.EventLogEntryType.Information:
                    break;
                case System.Diagnostics.EventLogEntryType.SuccessAudit:
                    severity = TraceSeverity.Verbose;
                    break;
                case System.Diagnostics.EventLogEntryType.Warning:
                    severity = TraceSeverity.Medium;
                    break;
                default:
                    break;
            }
            LoggingService.Current.WriteTrace(0, category, severity, format, source, message, type.ToString());
        }
    }

}

