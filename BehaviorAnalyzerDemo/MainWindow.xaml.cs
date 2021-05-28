using BADLibrary.Managers;
using BADLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BehaviorAnalyzerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<InstalledApp> uiApps;
        List<Process> uiProcesses;
        List<UserActivity> uiActivities;
        //Timer appTimer;
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }


        private void LoadData()
        {
            uiApps = AppManager.GetAppManager().GetAllAppsData();
            uiProcesses = ProcessManager.GetProcessManager().GetAllProcessData();
            uiActivities = UserActivityManager.GetUserActivityManager().GetAllUserActivityData();
            processesListBox.ItemsSource = uiProcesses;
            programsListBox.ItemsSource = uiApps;
            loginsListBox.ItemsSource = uiActivities;
        }
    }
}
