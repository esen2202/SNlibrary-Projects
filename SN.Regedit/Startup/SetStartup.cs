﻿namespace SN.Regedit.Startup
{
    public class SetStartup2
    {

        //- Startup registry key and value
        private static readonly string StartupBaseKey = "HKEY_CURRENT_USER\\";
        private static readonly string StartupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        // private static readonly string StartupValue = "EsenSoftScada";

        public static void SetStartup(string ExecutablePath, string StartupValue)
        {
            //if (Registry.GetValue(StartupBaseKey + StartupKey, StartupValue, null) == null)
            //{
            //    //- Set the application to run at startup
            //    RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupKey, true);
            //    key.SetValue(StartupValue, ExecutablePath);
            //}
        }

        public static void DeleteStartup(string StartupValue)
        {
            //if (Registry.GetValue(StartupBaseKey + StartupKey, StartupValue, null) != null)
            //{
            //    RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupKey, true);
            //    key.DeleteValue(StartupValue);
            //}
        }


    }

}
