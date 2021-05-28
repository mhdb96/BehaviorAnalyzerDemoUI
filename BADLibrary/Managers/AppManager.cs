using BADLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace BADLibrary.Managers
{
    public class AppManager
    {
        private static readonly AppManager _instance = new AppManager();
        List<InstalledApp> apps;
        private AppManager()
        {
            apps = new List<InstalledApp>();
        }
        public static AppManager GetAppManager()
        {
            return _instance;
        }
        public List<InstalledApp> GetAllAppsData()
        {
            apps.Clear();
            RegistryKey regKey = Registry.LocalMachine;
            RegistryKey subKey1 = regKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            string[] subKeyNames = subKey1.GetSubKeyNames();
            foreach (string subKeyName in subKeyNames)
            {
                RegistryKey subKey2 = subKey1.OpenSubKey(subKeyName);
                if (ValueNameExists(subKey2.GetValueNames(), "DisplayName") &&
                ValueNameExists(subKey2.GetValueNames(), "DisplayVersion"))
                {
                    InstalledApp app = new InstalledApp();
                    app.Name = subKey2.GetValue("DisplayName").ToString();
                    apps.Add(app);
                }
                subKey2.Close();
            }
            subKey1.Close();
            apps.Sort((x, y) => x.Name.CompareTo(y.Name));
            return apps;
        }
        private bool ValueNameExists(string[] valueNames, string valueName)
        {
            foreach (string s in valueNames)
            {
                if (s.ToLower() == valueName.ToLower()) return true;
            }
            return false;
        }
    }
}
