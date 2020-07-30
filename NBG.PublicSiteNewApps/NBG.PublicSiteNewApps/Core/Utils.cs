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
    }
}
