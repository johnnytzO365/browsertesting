using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowInformation
{
    class Program
    {
        static void Main(string[] args)
        {
            string itemUrl = "http://vm-sp2013/english/contact/Pages/TestW.aspx";
            int index = itemUrl.LastIndexOf('/');
            string siteUrl = itemUrl.Substring(0, index - 6);
            using (ClientContext ctx = new ClientContext(siteUrl))
            {
                Utilities.Connect(ctx, siteUrl);
                ListItem item = Utilities.GetItem(ctx, itemUrl);
                //Utilities.GetCurrentState();
                //Utilities.GetPredictions();


                ListItemCollection events = Utilities.GetWorkflowHistory(ctx, item);
                              
                string stages = "";
                string stage1user = "";
                string stage2group = "";
                string stage2user = "";
                string stage3group = "";
                string stage3user = "";
                string stage4group = "";
                string stage4user = "";
                string segMessage = "";
                for (int i = 0; i < events.Count; i++ )
                {
                    ListItem ev = events[i];
                    string outcome = "";
                    if (ev["Outcome"] != null)
                        outcome = ev["Outcome"].ToString();
                    string description = "";
                    if (ev["Description"] != null)
                        description = ev["Description"].ToString();
                    string user = "";
                    if (ev["User"] != null)
                    {
                        FieldUserValue user1 = (FieldUserValue)ev["User"];
                        user = user1.LookupValue;
                    }

                    if (description.StartsWith("Executing"))
                    {
                        stages = description;
                        stage1user = user;
                    }

                    if(description.StartsWith("Segregation of Duty"))
                    {
                        string prevOutcome = "";
                        int segStage;
                        string checkOutGroup = "";
                        string approveGroup = "";
                        int j = 1;
                        while(true)
                        {
                            if (events[i - j]["Outcome"] != null)
                            {
                                prevOutcome = events[i - j]["Outcome"].ToString();
                                break;
                            }
                            j++;
                        }
                        if(prevOutcome.Equals("Approval"))
                        {
                            segStage = 2;
                            checkOutGroup = stage2group;
                            //approveGroup = stage1group;
                        }
                        else if(prevOutcome.Equals("Audit"))
                        {
                            segStage = 3;
                            checkOutGroup = stage3group;
                            approveGroup = stage2group;
                        }
                        else
                        {
                            segStage = 4;
                            checkOutGroup = stage4group;
                            approveGroup = stage3group;
                        }

                        FieldUserValue user1 = (FieldUserValue)events[i + 1]["User"];
                        user = user1.LookupValue;
                        segMessage = "Stage " + segStage + " segregation by user " + user;
                        segMessage += "\nSteps: 1) By group " + checkOutGroup + ", except user " + user + ": check out and check in (major version).";
                        segMessage += "\n2) By group " + approveGroup + ": approve.";
                        segMessage += "\n3) By group " + checkOutGroup + ": approve.";

                        break;
                    }                        

                    if (outcome.Equals("Approval"))
                    {
                        int ind = description.IndexOf(':');
                        stage2group = description.Substring(ind, description.Length - ind);
                    }
                    else if (outcome.Equals("Audit"))
                    {
                        int ind = description.IndexOf(':');
                        stage3group = description.Substring(ind, description.Length - ind);
                    }
                    else if(outcome.Equals("Publish"))
                    {
                        int ind = description.IndexOf(':');
                        stage4group = description.Substring(ind, description.Length - ind);
                    }
                    else if (outcome.Equals("Approved"))
                    {
                        string prevOutcome = "";
                        int j = 1;
                        while(true)
                        {
                            if (events[i - j]["Outcome"] != null)
                            {
                                prevOutcome = events[i - j]["Outcome"].ToString();
                                break;
                            }
                            j++;
                        }
                        if (prevOutcome.Equals("Approval"))
                        {
                            FieldUserValue user1 = (FieldUserValue)events[i - 1]["User"];
                            user = user1.LookupValue;
                            stage2user = user;
                        }
                        if (prevOutcome.Equals("Audit"))
                        {
                            FieldUserValue user1 = (FieldUserValue)events[i - 1]["User"];
                            user = user1.LookupValue;
                            stage3user = user;
                        }
                        if (prevOutcome.Equals("Publish"))
                        {
                            FieldUserValue user1 = (FieldUserValue)events[i - 1]["User"];
                            user = user1.LookupValue;
                            stage4user = user;
                        }
                    }

                }

                if(!stages.Equals(""))
                    Console.WriteLine(stages);
                if (!stage1user.Equals(""))
                Console.WriteLine("Stage 1: " + stage1user);
                if (!stage2group.Equals(""))
                    Console.WriteLine("Stage 2 by group " + stage2group);
                if (!stage2user.Equals(""))
                    Console.WriteLine("Stage 2 approved by: " + stage2user);
                if (!stage3group.Equals(""))
                    Console.WriteLine("Stage 3 by group " + stage3group);
                if (!stage3user.Equals(""))
                    Console.WriteLine("Stage 3 approved by: " + stage3user);
                if (!stage4group.Equals(""))
                    Console.WriteLine("Stage 4 by group " + stage4group);
                if (!stage4user.Equals(""))
                    Console.WriteLine("Stage 4 approved by: " + stage4user);
                if (!segMessage.Equals(""))
                    Console.WriteLine(segMessage);

                Console.ReadLine();
            }

        }
    }
}
