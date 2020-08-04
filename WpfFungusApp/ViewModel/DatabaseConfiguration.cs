using System;
using Microsoft.Win32;

namespace WpfFungusApp.ViewModel
{
    internal class DatabaseConfiguration : IDatabaseConfiguration
    {
        public DatabaseConfiguration()
        {
            SelectedDatabaseProvider = DBStore.DatabaseProvider.SQLite;

            SQLServer_UseWindowsAuthentication = true;
            SQLServer_UseLocalServer = true;
            SQLServer_IPAddress = "127.0.0.1";
            SQLServer_UseIPv6 = false;
            SQLServer_Port = 1433;
            SQLServer_UseWindowsAuthentication = true;

            PostgreSQL_Host = "127.0.0.1";
            PostgreSQL_UseIPv6 = false;
            PostgreSQL_Port = 5432;
            PostgreSQL_UseWindowsAuthentication = true;

            MySQL_Host = "127.0.0.1";
            MySQL_UseIPv6 = false;
            MySQL_Port = 3306;
            MySQL_UseWindowsAuthentication = true;
        }

        private string _keyPath = System.Environment.Is64BitOperatingSystem ? @"SOFTWARE\Wow6432Node\WpfFungusApp\DatabaseSettings" : @"SOFTWARE\WpfFungusApp\DatabaseSettings";

        private string Load(Microsoft.Win32.RegistryKey key, string name, string defaultValue)
        {
            Object obj = key.GetValue(name, defaultValue);
            if (obj != null)
            {
                defaultValue = Convert.ToString(obj);
            }
            return defaultValue;
        }

        private bool Load(Microsoft.Win32.RegistryKey key, string name, bool defaultValue)
        {
            Object obj = key.GetValue(name, defaultValue);
            if (obj != null)
            {
                defaultValue = Convert.ToBoolean(obj);
            }
            return defaultValue;
        }

        private ushort Load(Microsoft.Win32.RegistryKey key, string name, ushort defaultValue)
        {
            Object obj = key.GetValue(name, defaultValue);
            if (obj != null)
            {
                defaultValue = Convert.ToUInt16(obj);
            }
            return defaultValue;
        }

        public bool Load()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(_keyPath);
            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(_keyPath);
            }
            else
            {
                SQLite_Folder = Load(key, "SQLite_Folder", SQLite_Folder);
                SQLite_Filename = Load(key, "SQLite_Filename", SQLite_Filename);

                SQLServer_UseLocalServer = Load(key, "SQLServer_UseLocalServer", SQLServer_UseLocalServer);
                SQLServer_IPAddress = Load(key, "SQLServer_IPAddress", SQLServer_IPAddress);
                SQLServer_UseIPv6 = Load(key, "SQLServer_UseIPv6", SQLServer_UseIPv6);
                SQLServer_Port = Load(key, "SQLServer_Port", SQLServer_Port);
                SQLServer_UseWindowsAuthentication = Load(key, "SQLServer_UseWindowsAuthentication", SQLServer_UseWindowsAuthentication);
                SQLServer_Folder = Load(key, "SQLServer_Folder", SQLServer_Folder);
                SQLServer_Filename = Load(key, "SQLServer_Filename", SQLServer_Filename);
                SQLServer_DatabaseName = Load(key, "SQLServer_DatabaseName", SQLServer_DatabaseName);

                PostgreSQL_Host = Load(key, "PostgreSQL_Host", PostgreSQL_Host);
                PostgreSQL_UseIPv6 = Load(key, "PostgreSQL_UseIPv6", PostgreSQL_UseIPv6);
                PostgreSQL_Port = Load(key, "PostgreSQL_Port", PostgreSQL_Port);
                PostgreSQL_UseWindowsAuthentication = Load(key, "PostgreSQL_UseWindowsAuthentication", PostgreSQL_UseWindowsAuthentication);
                PostgreSQL_DatabaseName = Load(key, "PostgreSQL_DatabaseName", PostgreSQL_DatabaseName);

                MySQL_Host = Load(key, "MySQL_Host", MySQL_Host);
                MySQL_UseIPv6 = Load(key, "MySQL_UseIPv6", MySQL_UseIPv6);
                MySQL_Port = Load(key, "MySQL_Port", MySQL_Port);
                MySQL_UseWindowsAuthentication = Load(key, "MySQL_UseWindowsAuthentication", MySQL_UseWindowsAuthentication);
                MySQL_DatabaseName = Load(key, "MySQL_DatabaseName", MySQL_DatabaseName);

                key.Close();
            }

            return true;
        }

        private void Save(Microsoft.Win32.RegistryKey key, string name, string value)
        {
            if (value != null)
            {
                key.SetValue(name, value);
            }
        }

        private void Save(Microsoft.Win32.RegistryKey key, string name, bool value)
        {
            key.SetValue(name, value);
        }

        private void Save(Microsoft.Win32.RegistryKey key, string name, ushort value)
        {
            key.SetValue(name, value);
        }

        public bool Save()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(_keyPath, true);
            if (key == null)
            {
                return false;
            }

            Save(key, "SQLite_Folder", SQLite_Folder);
            Save(key, "SQLite_Filename", SQLite_Filename);
            Save(key, "SQLServer_UseLocalServer", SQLServer_UseLocalServer);
            Save(key, "SQLServer_IPAddress", SQLServer_IPAddress);
            Save(key, "SQLServer_UseIPv6", SQLServer_UseIPv6);
            Save(key, "SQLServer_Port", SQLServer_Port);
            Save(key, "SQLServer_UseWindowsAuthentication", SQLServer_UseWindowsAuthentication);
            Save(key, "SQLServer_Folder", SQLServer_Folder);
            Save(key, "SQLServer_Filename", SQLServer_Filename);
            Save(key, "SQLServer_DatabaseName", SQLServer_DatabaseName);
            Save(key, "PostgreSQL_Host", PostgreSQL_Host);
            Save(key, "PostgreSQL_UseIPv6", PostgreSQL_UseIPv6);
            Save(key, "PostgreSQL_Port", PostgreSQL_Port);
            Save(key, "PostgreSQL_UseWindowsAuthentication", PostgreSQL_UseWindowsAuthentication);
            Save(key, "PostgreSQL_DatabaseName", PostgreSQL_DatabaseName);
            Save(key, "MySQL_Host", MySQL_Host);
            Save(key, "MySQL_UseIPv6", MySQL_UseIPv6);
            Save(key, "MySQL_Port", MySQL_Port);
            Save(key, "MySQL_UseWindowsAuthentication", MySQL_UseWindowsAuthentication);
            Save(key, "MySQL_DatabaseName", MySQL_DatabaseName);

            key.Close();
            return true;
        }

        #region IDatabaseConfiguration

        public DBStore.DatabaseProvider SelectedDatabaseProvider { get; set; }
        public string SQLite_Folder { get; set; }
        public string SQLite_Filename { get; set; }

        public int SelectedSqlServerInstance { get; set; }
        public bool SQLServer_UseLocalServer { get; set; }
        public string SQLServer_IPAddress { get; set; }
        public bool SQLServer_UseIPv6 { get; set; }
        public ushort SQLServer_Port { get; set; }
        public bool SQLServer_UseWindowsAuthentication { get; set; }
        // Not persisted
        public string SQLServer_UserName { get; set; }
        // Not persisted
        public string SQLServer_Password { get; set; }
        public string SQLServer_Folder { get; set; }
        public string SQLServer_Filename { get; set; }
        public string SQLServer_DatabaseName { get; set; }

        public string PostgreSQL_Host { get; set; }
        public bool PostgreSQL_UseIPv6 { get; set; }
        public ushort PostgreSQL_Port { get; set; }
        public bool PostgreSQL_UseWindowsAuthentication { get; set; }
        // Not persisted
        public string PostgreSQL_UserName { get; set; }
        // Not persisted
        public string PostgreSQL_Password { get; set; }
        public string PostgreSQL_DatabaseName { get; set; }

        public string MySQL_Host { get; set; }
        public bool MySQL_UseIPv6 { get; set; }
        public ushort MySQL_Port { get; set; }
        public bool MySQL_UseWindowsAuthentication { get; set; }
        // Not persisted
        public string MySQL_UserName { get; set; }
        // Not persisted
        public string MySQL_Password { get; set; }
        public string MySQL_DatabaseName { get; set; }

        #endregion IDatabaseConfiguration
    }
}
