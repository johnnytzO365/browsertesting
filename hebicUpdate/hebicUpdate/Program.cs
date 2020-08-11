using CsvHelper;
using Microsoft.SharePoint.Client;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace Hebic_update
{
    class Program
    {
        static void Main(string[] args)
        {
            //configure log file
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "log.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            LogManager.Configuration = config;
            Logger _log_ = NLog.LogManager.GetCurrentClassLogger();

            //connect
            var siteUrl = ConfigurationManager.AppSettings["siteUrl"];
            using (ClientContext clientContext = new ClientContext(siteUrl))
            {
                Console.WriteLine("Enter username:");
                string username = Console.ReadLine();
                Console.WriteLine("Enter password:");
                string password = "";
                //mask the password with * in console
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        password += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);
                try
                {
                    NetworkCredential _myCredentials = new NetworkCredential(username, password);
                    clientContext.Credentials = _myCredentials;
                    clientContext.ExecuteQuery();
                    Console.WriteLine("Connected to {0}", siteUrl);
                    _log_.Info("Connected to {0}", siteUrl);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Couldn't connect to {0} with your credentials!", siteUrl);
                    _log_.Error("Couldn't connect to {0} with your credentials!: {1}", siteUrl,ex);
                    Environment.Exit(-1);
                }

                try
                {
                    var site = clientContext.Site;
                    var web = clientContext.Site.RootWeb;
                    clientContext.Load(web, w => w.ServerRelativeUrl);
                    clientContext.ExecuteQuery();
                }
                catch(Exception ex)
                {
                    _log_.Error("Couldn't load site: {0}", ex);
                }
                //read the csv files
                //BANKS
                List<string> Codes = new List<string>();
                List<string> Names = new List<string>();
                List<string> Regions = new List<string>();
                List<string> Bics = new List<string>();
                List<string> Tels = new List<string>();
                List<string> Faxes = new List<string>();
                List<string> WebSites = new List<string>();
                try
                {
                    Console.WriteLine("Reading banks Csv...");
                    using (var reader = new StreamReader(ConfigurationManager.AppSettings["bankCSV"], Encoding.Default))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Configuration.Delimiter = ";";
                        csv.Configuration.HasHeaderRecord = true;
                        csv.Read();
                        csv.ReadHeader();
                        while (csv.Read())
                        {
                            string code = csv.GetField<string>("ΑΡΙΘΜΗΤΙΚΟΣ ΚΩΔΙΚΟΣ ΤΗΣ ΤΡΑΠΕΖΑΣ");
                            if (code == "")
                            {
                                code = "-";
                            }
                            else if (code.Length == 2)
                            {
                                code = "0" + code;
                            }
                            Codes.Add(code);

                            string name = csv.GetField<string>("ΕΠΙΣΗΜΗ ΟΝΟΜΑΣΙΑ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)");
                            if (name == "")
                            {
                                name = "-";
                            }
                            Names.Add(name);

                            string region = csv.GetField<string>("ΔΙΕΥΘΥΝΣΗ ΕΔΡΑΣ ΤΗΣ ΤΡΑΠΕΖΑΣ (ΕΛΛΗΝΙΚΑ)");
                            if (region == "")
                            {
                                region = "-";
                            }
                            Regions.Add(region);

                            string bic = csv.GetField<string>("SWIFT BIC");
                            if (bic == "")
                            {
                                bic = "-";
                            }
                            Bics.Add(bic);

                            string tel = csv.GetField<string>("ΤΗΛΕΦΩΝΙΚΟ ΚΕΝΤΡΟ");
                            if (tel == "")
                            {
                                tel = "-";
                            }
                            Tels.Add(tel);

                            string fax = csv.GetField<string>("ΚΕΝΤΡΙΚΟFAX");
                            if (fax == "")
                            {
                                fax = "-";
                            }
                            Faxes.Add(fax);

                            string webSite = csv.GetField<string>("ΗΛΕΚΤΡΟΝΙΚΗ ΔΙΕΥΘΥΝΣΗ-URL");
                            if (webSite == "")
                            {
                                webSite = "-";
                            }
                            WebSites.Add(webSite);
                        }
                    }
                    _log_.Info("Banks csv read");
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Couldn't read {0}!", ConfigurationManager.AppSettings["bankCSV"]);
                    _log_.Error("Couldn't read {0}: {1}", ConfigurationManager.AppSettings["bankCSV"], ex);
                }

                //BRANCHES
                List<string> Hebics = new List<string>();
                List<string> BranchNames = new List<string>();
                List<string> BranchRegions = new List<string>();
                List<string> Addresses = new List<string>();
                List<string> BranchTels = new List<string>();
                List<string> Communities = new List<string>();
                List<string> Municipalities = new List<string>();
                List<string> ZipCodes = new List<string>();
                try
                {
                    Console.WriteLine("Reading branches Csv...");
                    using (var reader = new StreamReader(ConfigurationManager.AppSettings["branchCSV"], Encoding.Default))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Configuration.Delimiter = ";";
                        csv.Configuration.HasHeaderRecord = true;
                        csv.Read();
                        csv.ReadHeader();
                        while (csv.Read())
                        {
                            string hebic = csv.GetField<string>("ΚΩΔΙΚΟΣ HEBIC");
                            if (hebic == "")
                            {
                                hebic = "-";
                            }
                            else if (hebic.Length == 6)
                            {
                                hebic = "0" + hebic;
                            }
                            Hebics.Add(hebic);

                            string name = csv.GetField<string>("ΟΝΟΜΑΣΙΑ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)");
                            if (name == "")
                            {
                                name = "-";
                            }
                            BranchNames.Add(name);

                            string region = csv.GetField<string>("ΟΝΟΜΑΣΙΑ ΤΟΠΟΘΕΣΙΑΣ ΚΑΤΑΣΤΗΜΑΤΟΣ (ΕΛΛΗΝΙΚΑ)");
                            if (region == "")
                            {
                                region = "-";
                            }
                            BranchRegions.Add(region);

                            string address = csv.GetField<string>("Διεύθυνση ΕΛΛΗΝΙΚΑ");
                            if (address == "")
                            {
                                address = "-";
                            }
                            Addresses.Add(address);

                            string tel = csv.GetField<string>("ΑΡΙΘΜΟΣ ΤΗΛΕΦΩΝΟΥ");
                            if (tel == "")
                            {
                                tel = "-";
                            }
                            BranchTels.Add(tel);

                            string community = csv.GetField<string>("ΤΑΧΥΔΡΟΜΙΚΗ ΠΕΡΙΟΧΗ (ΕΛΛΗΝΙΚΑ)");
                            if (community == "")
                            {
                                community = "-";
                            }
                            Communities.Add(community);

                            string municipality = csv.GetField<string>("ΔΗΜΟΣ/ ΚΟΙΝΟΤΗΤΑ");
                            if (municipality == "")
                            {
                                municipality = "-";
                            }
                            Municipalities.Add(municipality);

                            string zipcode = "";
                            string temp1 = csv.GetField<string>("ΓΡΑΦΕΙΟ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ");
                            string temp2 = csv.GetField<string>("ΔΙΑΔΡΟΜΗ ΤΑΧΥΔΡΟΜΙΚΟΥ ΚΩΔΙΚΑ");
                            if (temp1.Length == 1)
                            {
                                temp1 = "00" + temp1;
                            }
                            else if (temp1.Length == 2)
                            {
                                temp1 = "0" + temp1;
                            }
                            if (temp2.Length == 1)
                            {
                                temp2 = "0" + temp2;
                            }
                            if (temp1 == "" && temp2 == "")
                            {
                                zipcode = "-";
                            }
                            else
                            {
                                zipcode = temp1 + temp2;
                            }
                            ZipCodes.Add(zipcode);
                        }
                    }
                    _log_.Info("Branches csv read");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Couldn't read {0}!", ConfigurationManager.AppSettings["bankCSV"]);
                    _log_.Error("Couldn't read {0}: {1}", ConfigurationManager.AppSettings["bankCSV"], ex);
                }

                //read BANKS list
                List listBanks = clientContext.Web.Lists.GetByTitle("BANKS");
                try
                {
                    clientContext.Load(listBanks);
                    clientContext.ExecuteQuery();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Couldn't load list Banks!");
                    _log_.Error("Couldn't load list Banks: {0}", ex);
                }
                if (listBanks != null)
                {
                    CamlQuery query = new CamlQuery();
                    query.ViewXml = "<View><Query><OrderBy><FieldRef Name='bankCode' Ascending='True' /></OrderBy></Query></View>";
                    ListItemCollection collListItem = listBanks.GetItems(query);
                    try
                    {
                        clientContext.Load(collListItem);
                        clientContext.ExecuteQuery();
                    }
                    catch(Exception ex)
                    {
                        _log_.Error("Couldn't get list items of list Banks: {0}",ex);
                    }

                    int l = 0;
                    int c = 0;
                    int lstop = collListItem.Count;
                    int cstop = Codes.Count;
                    Console.WriteLine("Updating Banks...");
                    while(c < cstop)
                    {
                        string sharepointCode = collListItem[l]["bankCode"].ToString();
                        bool flag = false;
                        if(Codes[c] == sharepointCode)
                        {
                            string sharepointName = collListItem[l]["bankName"].ToString();
                            string csvName = Names[c];
                            if (!(collListItem[l]["bankName"].ToString().Equals(Names[c])))
                            {
                                collListItem[l]["bankName"] = Names[c];
                                flag = true;
                            }
                            if (!(collListItem[l]["bankRegion"].ToString().Equals(Regions[c])))
                            {
                                collListItem[l]["bankRegion"] = Regions[c];
                                flag = true;
                            }
                            if (!(collListItem[l]["bankBic"].ToString().Equals(Bics[c])))
                            {
                                collListItem[l]["bankBic"] = Bics[c];
                                flag = true;
                            }
                            if (!(collListItem[l]["bankTel"].ToString().Equals(Tels[c])))
                            {
                                collListItem[l]["bankTel"] = Tels[c];
                                flag = true;
                            }
                            if (!(collListItem[l]["bankFax"].ToString().Equals(Faxes[c])))
                            {
                                collListItem[l]["bankFax"] = Faxes[c];
                                flag = true;
                            }
                            if (!(collListItem[l]["bankWebSite"].ToString().Equals(WebSites[c])))
                            {
                                collListItem[l]["bankWebSite"] = WebSites[c];
                                flag = true;
                            }
                            if (flag)
                            {
                                try
                                {
                                    collListItem[l].Update();
                                    clientContext.ExecuteQuery();
                                }
                                catch(Exception ex)
                                {
                                    _log_.Error("Couldn't update bank item, c={0}, l={1}: {2}",c,l,ex);
                                }
                            }
                            l++;
                            c++;
                        }
                        else if(Codes[c].CompareTo(sharepointCode) > 0 )
                        {
                            try
                            {
                                collListItem[l].DeleteObject();
                                clientContext.ExecuteQuery();
                            }
                            catch (Exception ex)
                            {
                                _log_.Error("Couldn't delete bank item, c={0}, l={1}: {2}", c, l, ex);
                            }
                            lstop--;
                        }
                        else
                        {
                            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                            ListItem newListItem = listBanks.AddItem(itemCreateInfo);
                            newListItem["bankCode"] = Codes[c];
                            newListItem["bankName"] = Names[c];
                            newListItem["bankRegion"] = Regions[c];
                            newListItem["bankBic"] = Bics[c];
                            newListItem["bankTel"] = Tels[c];
                            newListItem["bankFax"] = Faxes[c];
                            newListItem["bankWebSite"] = WebSites[c];

                            try
                            {
                                newListItem.Update();
                                clientContext.ExecuteQuery();
                            }
                            catch (Exception ex)
                            {
                                _log_.Error("Couldn't add new bank item, c={0}, l={1}: {2}", c, l, ex);
                            }
                            c++;
                        }

                        if (l == lstop)
                            break;
                    }

                    if (c == cstop)
                    {
                        for(int i=l;i<lstop; i++)
                        {
                            try
                            {
                                collListItem[l].DeleteObject();
                                clientContext.ExecuteQuery();
                            }
                            catch (Exception ex)
                            {
                                _log_.Error("Couldn't delete bank item (c==cstop), c={0} l={1} {2}", c, l, ex);
                            }
                        }
                    }
                    if(l == lstop)
                    {
                        for(int i=c; i<cstop; i++)
                        {
                            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                            ListItem newListItem = listBanks.AddItem(itemCreateInfo);
                            newListItem["bankCode"] = Codes[i];
                            newListItem["bankName"] = Names[i];
                            newListItem["bankRegion"] = Regions[i];
                            newListItem["bankBic"] = Bics[i];
                            newListItem["bankTel"] = Tels[i];
                            newListItem["bankFax"] = Faxes[i];
                            newListItem["bankWebSite"] = WebSites[i];

                            try
                            {
                                newListItem.Update();
                                clientContext.ExecuteQuery();
                            }
                            catch (Exception ex)
                            {
                                _log_.Error("Couldn't add new bank item (l==lstop), c={0}, l={1}: {2}", c, l, ex);
                            }
                        }
                    }
                    _log_.Info("Banks List Updated");
                }



                //read BRANCHES list
                List listBranches = clientContext.Web.Lists.GetByTitle("BRANCHES");
                try
                {
                    clientContext.Load(listBranches);
                    clientContext.ExecuteQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Couldn't load list Branches!");
                    _log_.Error("Couldn't load list Branches: {0}", ex);
                }
                if (listBranches != null)
                {
                    CamlQuery query = new CamlQuery();
                    query.ViewXml = "<View><Query><OrderBy><FieldRef Name='branchHebic' Ascending='True' /></OrderBy></Query></View>";
                    ListItemCollection collListItem = listBranches.GetItems(query);
                    try
                    {
                        clientContext.Load(collListItem);
                        clientContext.ExecuteQuery();
                    }
                    catch (Exception ex)
                    {
                        _log_.Error("Couldn't get list items of list Branches: {0}", ex);
                    }

                    int l = 0;
                    int c = 0;
                    int lstop= collListItem.Count;
                    int cstop = Hebics.Count;
                    int mycount = 100;
                    Console.WriteLine("Updating Branches...");
                    while(c < cstop)
                    {
                        if(c%100==99)
                        {
                            Console.WriteLine(" {0} items processed", mycount);
                            mycount += 100;
                        }
                        string sharepointHebic = collListItem[l]["branchHebic"].ToString();
                        if(Hebics[c] == sharepointHebic)
                        {
                            bool flag = false;
                            string sharepointName = collListItem[l]["branchName"].ToString();
                            string csvName = BranchNames[c];
                            if (!(collListItem[l]["branchName"].ToString().Equals(BranchNames[c])))
                            {
                                collListItem[l]["branchName"] = BranchNames[c];
                                flag = true;
                            }
                            if (!(collListItem[l]["branchRegion"].ToString().Equals(BranchRegions[c])))
                            {
                                collListItem[l]["branchRegion"] = BranchRegions[c];
                                flag = true;
                            }
                            if (!(collListItem[l]["branchAddress"].ToString().Equals(Addresses[c])))
                            {
                                collListItem[l]["branchAddress"] = Addresses[c];
                                flag = true;
                            }
                            if (!(collListItem[l]["branchTel"].ToString().Equals(BranchTels[c])))
                            {
                                collListItem[l]["branchTel"] = BranchTels[c];
                                flag = true;
                            }
                            if (!(collListItem[l]["branchCommunity"].ToString().Equals(Communities[c])))
                            {
                                collListItem[l]["branchCommunity"] = Communities[c];
                                flag = true;
                            }
                            if (!(collListItem[l]["branchMunicipality"].ToString().Equals(Municipalities[c])))
                            {
                                collListItem[l]["branchMunicipality"] = Municipalities[c];
                                flag = true;
                            }
                            if (!(collListItem[l]["branchZipCode"].ToString().Equals(ZipCodes[c])))
                            {
                                collListItem[l]["branchZipCode"] = ZipCodes[c];
                                flag = true;
                            }
                            if (flag)
                            {
                                try
                                {
                                    collListItem[l].Update();
                                    clientContext.ExecuteQuery();
                                }
                                catch (Exception ex)
                                {
                                    _log_.Error("Couldn't update branch item, c={0}, l={1}: {2}", c, l, ex);
                                }
                            }
                            l++;
                            c++;
                        }
                        else if(Hebics[c].CompareTo(sharepointHebic)>0)
                        {
                            try
                            {
                                collListItem[l].DeleteObject();
                                clientContext.ExecuteQuery();
                            }
                            catch (Exception ex)
                            {
                                _log_.Error("Couldn't delete branch item, c={0}, l={1}: {2}", c, l, ex);
                            }
                            lstop--;
                        }
                        else
                        {
                            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                            ListItem newListItem = listBranches.AddItem(itemCreateInfo);
                            newListItem["branchHebic"] = Hebics[c];
                            newListItem["branchName"] = BranchNames[c];
                            newListItem["branchRegion"] = BranchRegions[c];
                            newListItem["branchAddress"] = Addresses[c];
                            newListItem["branchTel"] = BranchTels[c];
                            newListItem["branchCommunity"] = Communities[c];
                            newListItem["branchMunicipality"] = Municipalities[c];
                            newListItem["branchZipCode"] = ZipCodes[c];

                            try
                            {
                                newListItem.Update();
                                clientContext.ExecuteQuery();
                            }
                            catch (Exception ex)
                            {
                                _log_.Error("Couldn't add new branch item, c={0}, l={1}: {2}", c, l, ex);
                            }
                            c++;
                        }
                        if (l == lstop)
                            break;
                    }
                    if(c==cstop)
                    {
                        for(int i=l;i<lstop;i++)
                        {
                            try
                            {
                                collListItem[l].DeleteObject();
                                clientContext.ExecuteQuery();
                            }
                            catch (Exception ex)
                            {
                                _log_.Error("Couldn't delete branch item (c==cstop), c={0}, l={1}: {2}", c, l, ex);
                            }
                        }
                    }
                    if(l==lstop)
                    {
                        for(int i=c;i<cstop;i++)
                        {
                            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                            ListItem newListItem = listBranches.AddItem(itemCreateInfo);
                            newListItem["branchHebic"] = Hebics[i];
                            newListItem["branchName"] = BranchNames[i];
                            newListItem["branchRegion"] = BranchRegions[i];
                            newListItem["branchAddress"] = Addresses[i];
                            newListItem["branchTel"] = BranchTels[i];
                            newListItem["branchCommunity"] = Communities[i];
                            newListItem["branchMunicipality"] = Municipalities[i];
                            newListItem["branchZipCode"] = ZipCodes[i];

                            try
                            {
                                newListItem.Update();
                                clientContext.ExecuteQuery();
                            }
                            catch (Exception ex)
                            {
                                _log_.Error("Couldn't add new branch item (l==lstop), c={0}, l={1}: {2}", c, l, ex);
                            }
                        }
                    }
                    _log_.Info("Branches List Updated");
                }
            }
        }
    }
}
