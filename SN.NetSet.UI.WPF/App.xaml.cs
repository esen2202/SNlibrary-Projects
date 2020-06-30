using Microsoft.Win32;
using SN.NetSet.UI.WPF.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SN.NetSet.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Process proc = Process.GetCurrentProcess();
            int count = Process.GetProcesses().Where(p =>
                p.ProcessName == proc.ProcessName).Count();

            if (count > 1)
            {
                App.Current.Shutdown();
            }

            SystemEvents.SessionEnding += SystemEvents_SessionEnding;
        }

        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            //WPF.Views.MainWindow.SetSettings();
            App.Current.Shutdown();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
           
        }
    }
}
