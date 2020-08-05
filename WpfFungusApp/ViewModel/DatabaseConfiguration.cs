using System;
using Microsoft.Win32;

namespace WpfFungusApp.ViewModel
{
    internal class DatabaseConfiguration : ConfigurationSerialiser, IDatabaseConfiguration
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

            PostgreSQL_IPAddress = "127.0.0.1";
            PostgreSQL_UseIPv6 = false;
            PostgreSQL_Port = 5432;
            PostgreSQL_UseWindowsAuthentication = true;

            MySQL_IPAddress = "127.0.0.1";
            MySQL_UseIPv6 = false;
            MySQL_Port = 3306;
            MySQL_UseWindowsAuthentication = true;
        }

        private string _keyPath = System.Environment.Is64BitOperatingSystem ? @"SOFTWARE\Wow6432Node\WpfFungusApp\DatabaseSettings" : @"SOFTWARE\WpfFungusApp\DatabaseSettings";

        public bool Load()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(_keyPath);
            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(_keyPath);
            }
            else
            {
                CurrentRegistryKey = key;

                SelectedDatabaseProvider = ReadEntry<DBStore.DatabaseProvider>("SelectedDatabaseProvider", SelectedDatabaseProvider);
                SQLite_Folder = ReadEntry<string>("SQLite_Folder", SQLite_Folder);
                SQLite_Filename = ReadEntry<string>("SQLite_Filename", SQLite_Filename);
                SQLServer_UseLocalServer = ReadEntry<bool>("SQLServer_UseLocalServer", SQLServer_UseLocalServer);
                SQLServer_IPAddress = ReadEntry<string>("SQLServer_IPAddress", SQLServer_IPAddress);
                SQLServer_UseIPv6 = ReadEntry<bool>("SQLServer_UseIPv6", SQLServer_UseIPv6);
                SQLServer_Port = ReadEntry<ushort>("SQLServer_Port", SQLServer_Port);
                SQLServer_UseWindowsAuthentication = ReadEntry<bool>("SQLServer_UseWindowsAuthentication", SQLServer_UseWindowsAuthentication);
                SQLServer_Folder = ReadEntry<string>("SQLServer_Folder", SQLServer_Folder);
                SQLServer_Filename = ReadEntry<string>("SQLServer_Filename", SQLServer_Filename);
                SQLServer_DatabaseName = ReadEntry<string>("SQLServer_DatabaseName", SQLServer_DatabaseName);
                PostgreSQL_IPAddress = ReadEntry<string>("PostgreSQL_IPAddress", PostgreSQL_IPAddress);
                PostgreSQL_UseIPv6 = ReadEntry<bool>("PostgreSQL_UseIPv6", PostgreSQL_UseIPv6);
                PostgreSQL_Port = ReadEntry<ushort>("PostgreSQL_Port", PostgreSQL_Port);
                PostgreSQL_UseWindowsAuthentication = ReadEntry<bool>("PostgreSQL_UseWindowsAuthentication", PostgreSQL_UseWindowsAuthentication);
                PostgreSQL_DatabaseName = ReadEntry<string>("PostgreSQL_DatabaseName", PostgreSQL_DatabaseName);
                MySQL_IPAddress = ReadEntry<string>("MySQL_IPAddress", MySQL_IPAddress);
                MySQL_UseIPv6 = ReadEntry<bool>("MySQL_UseIPv6", MySQL_UseIPv6);
                MySQL_Port = ReadEntry<ushort>("MySQL_Port", MySQL_Port);
                MySQL_UseWindowsAuthentication = ReadEntry<bool>("MySQL_UseWindowsAuthentication", MySQL_UseWindowsAuthentication);
                MySQL_DatabaseName = ReadEntry<string>("MySQL_DatabaseName", MySQL_DatabaseName);

                key.Close();
            }

            return true;
        }

        public bool Save()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(_keyPath, true);
            if (key == null)
            {
                return false;
            }

            CurrentRegistryKey = key;

            WriteEntry<DBStore.DatabaseProvider>("SelectedDatabaseProvider", SelectedDatabaseProvider);
            WriteEntry<string>("SQLite_Folder", SQLite_Folder);
            WriteEntry<string>("SQLite_Folder", SQLite_Folder);
            WriteEntry<string>("SQLite_Filename", SQLite_Filename);
            WriteEntry<bool>("SQLServer_UseLocalServer", SQLServer_UseLocalServer);
            WriteEntry<string>("SQLServer_IPAddress", SQLServer_IPAddress);
            WriteEntry<bool>("SQLServer_UseIPv6", SQLServer_UseIPv6);
            WriteEntry<ushort>("SQLServer_Port", SQLServer_Port);
            WriteEntry<bool>("SQLServer_UseWindowsAuthentication", SQLServer_UseWindowsAuthentication);
            WriteEntry<string>("SQLServer_Folder", SQLServer_Folder);
            WriteEntry<string>("SQLServer_Filename", SQLServer_Filename);
            WriteEntry<string>("SQLServer_DatabaseName", SQLServer_DatabaseName);
            WriteEntry<string>("PostgreSQL_IPAddress", PostgreSQL_IPAddress);
            WriteEntry<bool>("PostgreSQL_UseIPv6", PostgreSQL_UseIPv6);
            WriteEntry<ushort>("PostgreSQL_Port", PostgreSQL_Port);
            WriteEntry<bool>("PostgreSQL_UseWindowsAuthentication", PostgreSQL_UseWindowsAuthentication);
            WriteEntry<string>("PostgreSQL_DatabaseName", PostgreSQL_DatabaseName);
            WriteEntry<string>("MySQL_IPAddress", MySQL_IPAddress);
            WriteEntry<bool>("MySQL_UseIPv6", MySQL_UseIPv6);
            WriteEntry<ushort>("MySQL_Port", MySQL_Port);
            WriteEntry<bool>("MySQL_UseWindowsAuthentication", MySQL_UseWindowsAuthentication);
            WriteEntry<string>("MySQL_DatabaseName", MySQL_DatabaseName);

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

        public string PostgreSQL_IPAddress { get; set; }
        public bool PostgreSQL_UseIPv6 { get; set; }
        public ushort PostgreSQL_Port { get; set; }
        public bool PostgreSQL_UseWindowsAuthentication { get; set; }
        // Not persisted
        public string PostgreSQL_UserName { get; set; }
        // Not persisted
        public string PostgreSQL_Password { get; set; }
        public string PostgreSQL_DatabaseName { get; set; }

        public string MySQL_IPAddress { get; set; }
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
