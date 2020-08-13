using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBG.PublicSiteNewApps.CommonLibrary.Log;

namespace NBG.PublicSiteNewApps.Core
{
    class Configuration
    {
        public static string QS_CACHE = "cache";
        public static string QS_CACHE_INVALIDATE = "invalidate";
        public static bool CONFIG_CACHE_SET = false;
        public static bool CONFIG_CACHE_ENABLED = false;
        public static int CONFIG_CACHE_DURATION = 0;
        public static string APPLICATION_NAME_EMAILSUBJECT = "nbg.gr";
        public static string RESOURCES_NAME = "NBGSite.Resources";
        public static string ERROR_STRING_FORMAT = "ItemId: {0}\nProcedure:{1}\nData:{2}\nError:\n{3}\n";
        public static string APPLICATION_NAME = "NBG.PublicSite";

        public static string GetValue1(SPWeb spWeb, string key, string category)
        {
            return GetValue(spWeb, key, category, true);
        }

        public static string GetValue(SPWeb spWeb, string key, string category, bool isFirstValue)
        {
            string value = "";
            try
            {
                string cacheKey = string.Format("Config_{0}_{1}_{2}", category, key, isFirstValue);
                if (GetConfigurationCacheEnabled(spWeb) && System.Web.HttpRuntime.Cache != null && System.Web.HttpRuntime.Cache[cacheKey] != null)
                {
                    if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null && System.Web.HttpContext.Current.Request.QueryString[QS_CACHE] == QS_CACHE_INVALIDATE)
                    {
                        // Do nothing, cache will be refreshed below
                    }
                    else
                        return System.Web.HttpRuntime.Cache[cacheKey] as string;
                }
                SPList spList = spWeb.Lists[ListNames.Configuration];
                SPQuery spQuery = new SPQuery();

                string query = @"
                    <Where> 
                        <And>
                            <And>
                                <Eq><FieldRef Name='{0}' /><Value Type='Text'>{1}</Value></Eq>
                                <Eq><FieldRef Name='{2}' /><Value Type='Text'>{3}</Value></Eq>
                            </And>
                            <Eq><FieldRef Name='{4}' /><Value Type='Text'>{5}</Value></Eq>
                        </And>
                    </Where>";
                spQuery.Query = string.Format(query, Fields.ConfigCategory, category, "Title", key, Fields.ItemIsActive, "1");

                SPListItemCollection spItems = spList.GetItems(spQuery);
                if (spItems.Count > 0)
                {
                    if (isFirstValue)
                        value = spItems[0][Fields.ConfigValue1] as string;
                    else
                        value = spItems[0][Fields.ConfigValue2] as string;
                }
                if (GetConfigurationCacheEnabled(spWeb) && System.Web.HttpRuntime.Cache != null && System.Web.HttpRuntime.Cache[cacheKey] != null)
                {
                    System.Web.HttpRuntime.Cache.Add(cacheKey, value, null, DateTime.Now.AddMinutes(Core.Configuration.GetConfigurationCacheDuration(spWeb)), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.NotRemovable, null);
                }
            }
            catch (Exception ex)
            {
                Logger.LogEvent(string.Format("Configuration.GetValue: Category: {0} Key: {1} 1stValue: {2}\n{3}", category, key, isFirstValue.ToString(), ex.ToString()), System.Diagnostics.EventLogEntryType.Warning);
            }
            return value;
        }

        public static string GetValue2(SPWeb spWeb, string key, string category)
        {
            return GetValue(spWeb, key, category, false);
        }

        public static int GetConfigurationCacheDuration(SPWeb spWeb)
        {
            if (!CONFIG_CACHE_SET)
            {
                // If necessary get value from SPWeb configuration list
                CONFIG_CACHE_DURATION = 180;
                CONFIG_CACHE_ENABLED = true;
                CONFIG_CACHE_SET = true;
            }
            return CONFIG_CACHE_DURATION;
        }

        public static bool GetConfigurationCacheEnabled(SPWeb spWeb)
        {
            if (!CONFIG_CACHE_SET)
            {
                // If necessary get value from SPWeb configuration list
                CONFIG_CACHE_DURATION = 180;
                CONFIG_CACHE_ENABLED = true;
                CONFIG_CACHE_SET = true;
            }
            return CONFIG_CACHE_ENABLED;
        }

        public static string GetSendEMails(SPWeb spWeb)
        {
            return GetValue(spWeb, ConfigurationKeys.SendEMails, ConfigurationCategories.Global, true);
        }

        public static class ListNames
        {
            public static string Configuration = "ResourcesConfiguration";
        }

        public static class ConfigurationKeys
        {
            public static string FormContact = "Form Contact";
            public static string FormEMailFrom = "Form EMail From";
            public static string SendEMails = "Send EMails";
        }

        public static class ConfigurationCategories
        {
            public static string Data = "Data";
            public static string Global = "Global";
        }

    }
}
