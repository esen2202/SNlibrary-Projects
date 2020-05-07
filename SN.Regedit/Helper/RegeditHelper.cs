using Microsoft.Win32;
using System.Linq;

namespace SN.Regedit.Startup
{
    public class RegeditHelper
    {
        private static readonly string StartupBaseKey = "HKEY_CURRENT_USER\\";
        private static readonly string StartupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public static void SetStartup(string ExecutablePath, string StartupValue)
        {
            if (Registry.GetValue(StartupBaseKey + StartupKey, StartupValue, null) == null)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupKey, true);
                key.SetValue(StartupValue, ExecutablePath);
            }
        }

        public static void DeleteStartup(string StartupValue)
        {
            if (Registry.GetValue(StartupBaseKey + StartupKey, StartupValue, null) != null)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupKey, true);
                key.DeleteValue(StartupValue);
            }
        }

        public static bool IsExistStartup(string StartupValue)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupKey, true);
            return (key.GetValueNames().Contains(StartupValue));
        }
    }
}