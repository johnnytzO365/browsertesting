using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowInformation
{
    class Utilities
    {
        public static void Connect(ClientContext ctx, string siteUrl)
        {
            string username = "spsetup";
            string password = "p@ssw0rd";
            try
            {
                NetworkCredential _myCredentials = new NetworkCredential(username, password);
                ctx.Credentials = _myCredentials;
                ctx.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't connect to " + siteUrl + " with your credentials! " + ex);
                Console.ReadLine();
                Environment.Exit(-1);
            }
        }

        public static ListItem GetItem(ClientContext ctx, string itemUrl)
        {
            string[] splits = itemUrl.Split('/');
            string listName = splits[splits.Length - 2];
            List List = ctx.Web.Lists.GetByTitle(listName);
            CamlQuery query = new CamlQuery();
            string serverRelativeUrl = itemUrl.Substring(16, itemUrl.Length - 16);
            query.ViewXml = "<View><Query><Where><Eq><FieldRef Name='FileRef'/><Value Type='Url'>" + serverRelativeUrl + "</Value></Eq></Where></Query></View>";
            ListItemCollection items = List.GetItems(query);
            ctx.Load(items);
            ctx.ExecuteQuery();

            ListItem item;
            if (items.Count > 0)
                item = items[0];
            else
                throw new Exception("Item not found!");
            ctx.Load(item, i => i["ID"]);
            ctx.ExecuteQuery();

            return item;
        }

        public static ListItemCollection GetWorkflowHistory(ClientContext ctx, ListItem item)
        {
            List WFHistory = ctx.Web.Lists.GetByTitle("Workflow History");
            CamlQuery query2 = new CamlQuery();
            query2.ViewXml = "<View><Query><Where><And><Eq><FieldRef Name='Outcome' /><Value Type='Text'>Workflow Started</Value></Eq><Eq><FieldRef Name='Item' /><Value Type='Integer'>" + item["ID"] + "</Value></Eq></And></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy></Query></View>";
            ListItemCollection startEvents = WFHistory.GetItems(query2);
            ctx.Load(startEvents);
            ctx.ExecuteQuery();

            ListItem startEvent;
            if (startEvents.Count > 0)
                startEvent = startEvents[0];
            else
                throw new Exception("Couldn't load workflow history events!");

            CamlQuery query3 = new CamlQuery();
            query3.ViewXml = "<View><Query><Where><Eq><FieldRef Name='WorkflowInstance' /><Value Type='Text'>" + startEvent["WorkflowInstance"].ToString() + "</Value></Eq></Where></Query></View>";
            ListItemCollection events = WFHistory.GetItems(query3);
            ctx.Load(events);
            ctx.ExecuteQuery();

            return events;
        }
    }
}
