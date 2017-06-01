using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using OVHDD.Classes;
using OVHDD.Windows;

namespace OVHDD
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //no double instance
            if(Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show(AppConstants.Name + " is already running.", "Error");
                Current.Shutdown();
                return;
            }

            //initialize settings
            AppSettings.InitializeSettings();

            //start home window
            new Home().Show();
        }
    }
}
