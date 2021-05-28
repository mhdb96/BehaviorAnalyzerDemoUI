using BADLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace BADLibrary.Managers
{
    public class UserActivityManager
    {
        private static readonly UserActivityManager _instance = new UserActivityManager();
        List<UserActivity> activities;
        private UserActivityManager()
        {
            activities = new List<UserActivity>();
        }
        public static UserActivityManager GetUserActivityManager()
        {
            return _instance;
        }
        public List<UserActivity> GetAllUserActivityData()
        {            
            //string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            string machineName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[0];            
            EventLog eventLog = new EventLog("Security", machineName);            
            foreach (EventLogEntry log in eventLog.Entries)
            {
                //if (log.InstanceId == 4634)
                //{

                //    //string[] stringSeparators = new string[] { "Logon Type:\t\t\t" };
                //    //Console.WriteLine($"Logout {log.Message.Split(stringSeparators, StringSplitOptions.None)[1][0]}");
                //    if (log.ReplacementStrings[4] == "7")
                //    {
                //        Console.WriteLine("logout" + log.TimeGenerated);
                //    }
                        
                //}
                //else 
                if (log.InstanceId == 4624)
                {
                    if (log.ReplacementStrings[8] == "2" || log.ReplacementStrings[8] == "11")
                    {
                        UserActivity ac = new UserActivity();
                        ac.DateTime = log.TimeGenerated;
                        if (log.ReplacementStrings[8] == "2")
                        {
                            ac.ActivityType = "Interactive Logon";
                        }
                        if (log.ReplacementStrings[8] == "11")
                        {
                            ac.ActivityType = "CachedInteractive Logon";
                        }
                        activities.Add(ac);
                    }                        
                }
            }            
            activities = activities.GroupBy(elem => elem.DateTime).Select(group => group.First()).ToList();
            activities = activities.Skip(Math.Max(0, activities.Count() - 5)).ToList();
            return activities;
        }
    }
}