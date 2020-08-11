using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBG.PublicSiteNewApps.Core
{
    class Utils
    {
        public static string RESOURCES_NAME = "NBGSite.Resources";

        public static string GetLocString(string resourceName)
        {
            return SPUtility.GetLocalizedString("$Resources:" + RESOURCES_NAME + "," + resourceName, RESOURCES_NAME, SPContext.Current.Web.Language);
        }

        public static string GetLocString(SPWeb spWeb, string resourceName)
        {
            return SPUtility.GetLocalizedString("$Resources:" + Core.Configuration.RESOURCES_NAME + "," + resourceName, Core.Configuration.RESOURCES_NAME, spWeb.Language);
        }

        public static string ConcatUrlPaths(string url1, string url2)
        {
            if (url1.EndsWith("/") && url2.StartsWith("/"))
                url1 = url1.Substring(0, url1.Length - 1);
            return string.Format("{0}{1}{2}", url1, ((url1.EndsWith("/") || url2.StartsWith("/")) ? "" : "/"), url2);
        }
    }
}
