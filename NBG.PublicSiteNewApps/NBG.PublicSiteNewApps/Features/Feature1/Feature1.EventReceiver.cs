using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using NBG.PublicSiteNewApps.CommonLibrary.Log;

namespace NBG.PublicSiteNewApps.Features.Feature1
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("fa937896-cb7f-49ba-8efb-49d0ac3f8a10")]
    public class Feature1EventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPSite site = properties.Feature.Parent as SPSite;
            SPWeb web = site.RootWeb;
            try
            {         
                //ToDo: Check if list exists 
                Guid guid = web.Lists.Add(Core.Configuration.ListNames.Configuration, Core.Configuration.ListNames.Configuration, SPListTemplateType.GenericList);                                                 
                SPList spList = web.Lists[guid];
                spList.Fields.Add("Config Value1", SPFieldType.Text, false);                
                spList.OnQuickLaunch = false;
                spList.Update();

                //Add field to default view
                spList = web.Lists[guid];
                SPField newfield = spList.Fields["Config Value1"];
                SPView defaultView = spList.DefaultView;
                defaultView.ViewFields.Add(newfield);
                defaultView.Update();

                spList = web.Lists[guid];
                SPListItem firstResource = spList.Items.Add();
                firstResource["Title"] = "ContactFormDropDownChoicesEl";
                firstResource["Config Value1"] = "Καταθέσεις;Κάρτες;Στεγαστικά Δάνεια;Καταναλωτικά Δάνεια;Εγγραφή στις υπηρεσίες i-bank;Χρηματιστηριακές συναλλαγές;Χρηματοδότηση Επαγγελματιών και ΜΜΕ;Προγράμματα ασφάλειας και φροντίδας;Intenet Banking";
                firstResource.Update();         

                SPListItem secondResource = spList.Items.Add();
                secondResource["Title"] = "ContactFormDropDownChoicesEn";
                secondResource["Config Value1"] = "Deposits;Cards;Housing Loans;Consumer Loans;Insuring & carding programs;Sign up for i-bank services;Share transactions;Small Business Financing";
                secondResource.Update();
            }
            catch (Exception ex)
            {
                //Logger.LogEvent(string.Format(Core.Configuration.ERROR_STRING_FORMAT, null, "CustomizeListsEventReceiver.CreateListSidebarBlocks", null, ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
                Logger.LogEvent(Logger.BuildLogString(null, "CustomizeListsEventReceiver.CreateListSidebarBlocks", web.Url, ex.ToString()), System.Diagnostics.EventLogEntryType.Error);
            }            
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
