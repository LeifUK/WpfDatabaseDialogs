﻿using System;
using Microsoft.Win32;

namespace WpfFungusApp.Model
{
    internal class DatabaseConfiguration : IDatabaseConfiguration
    {
        public DatabaseConfiguration(IConfigurationSerialiser iConfigurationSerialiser)
        {
            IConfigurationSerialiser = iConfigurationSerialiser;
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
        
        public readonly IConfigurationSerialiser IConfigurationSerialiser;

        public void Load()
        {
            SelectedDatabaseProvider = IConfigurationSerialiser.ReadEntry<DBStore.DatabaseProvider>("SelectedDatabaseProvider", SelectedDatabaseProvider);
            SQLite_Folder = IConfigurationSerialiser.ReadEntry<string>("SQLite_Folder", SQLite_Folder);
            SQLite_Filename = IConfigurationSerialiser.ReadEntry<string>("SQLite_Filename", SQLite_Filename);
            SQLServer_UseLocalServer = IConfigurationSerialiser.ReadEntry<bool>("SQLServer_UseLocalServer", SQLServer_UseLocalServer);
            SQLServer_IPAddress = IConfigurationSerialiser.ReadEntry<string>("SQLServer_IPAddress", SQLServer_IPAddress);
            SQLServer_UseIPv6 = IConfigurationSerialiser.ReadEntry<bool>("SQLServer_UseIPv6", SQLServer_UseIPv6);
            SQLServer_Port = IConfigurationSerialiser.ReadEntry<ushort>("SQLServer_Port", SQLServer_Port);
            SQLServer_UseWindowsAuthentication = IConfigurationSerialiser.ReadEntry<bool>("SQLServer_UseWindowsAuthentication", SQLServer_UseWindowsAuthentication);
            SQLServer_Folder = IConfigurationSerialiser.ReadEntry<string>("SQLServer_Folder", SQLServer_Folder);
            SQLServer_Filename = IConfigurationSerialiser.ReadEntry<string>("SQLServer_Filename", SQLServer_Filename);
            SQLServer_DatabaseName = IConfigurationSerialiser.ReadEntry<string>("SQLServer_DatabaseName", SQLServer_DatabaseName);
            PostgreSQL_IPAddress = IConfigurationSerialiser.ReadEntry<string>("PostgreSQL_IPAddress", PostgreSQL_IPAddress);
            PostgreSQL_UseIPv6 = IConfigurationSerialiser.ReadEntry<bool>("PostgreSQL_UseIPv6", PostgreSQL_UseIPv6);
            PostgreSQL_Port = IConfigurationSerialiser.ReadEntry<ushort>("PostgreSQL_Port", PostgreSQL_Port);
            PostgreSQL_UseWindowsAuthentication = IConfigurationSerialiser.ReadEntry<bool>("PostgreSQL_UseWindowsAuthentication", PostgreSQL_UseWindowsAuthentication);
            PostgreSQL_DatabaseName = IConfigurationSerialiser.ReadEntry<string>("PostgreSQL_DatabaseName", PostgreSQL_DatabaseName);
            MySQL_IPAddress = IConfigurationSerialiser.ReadEntry<string>("MySQL_IPAddress", MySQL_IPAddress);
            MySQL_UseIPv6 = IConfigurationSerialiser.ReadEntry<bool>("MySQL_UseIPv6", MySQL_UseIPv6);
            MySQL_Port = IConfigurationSerialiser.ReadEntry<ushort>("MySQL_Port", MySQL_Port);
            MySQL_UseWindowsAuthentication = IConfigurationSerialiser.ReadEntry<bool>("MySQL_UseWindowsAuthentication", MySQL_UseWindowsAuthentication);
            MySQL_DatabaseName = IConfigurationSerialiser.ReadEntry<string>("MySQL_DatabaseName", MySQL_DatabaseName);
        }

        public void Save()
        {
            IConfigurationSerialiser.WriteEntry<DBStore.DatabaseProvider>("SelectedDatabaseProvider", SelectedDatabaseProvider);
            IConfigurationSerialiser.WriteEntry<string>("SQLite_Folder", SQLite_Folder);
            IConfigurationSerialiser.WriteEntry<string>("SQLite_Folder", SQLite_Folder);
            IConfigurationSerialiser.WriteEntry<string>("SQLite_Filename", SQLite_Filename);
            IConfigurationSerialiser.WriteEntry<bool>("SQLServer_UseLocalServer", SQLServer_UseLocalServer);
            IConfigurationSerialiser.WriteEntry<string>("SQLServer_IPAddress", SQLServer_IPAddress);
            IConfigurationSerialiser.WriteEntry<bool>("SQLServer_UseIPv6", SQLServer_UseIPv6);
            IConfigurationSerialiser.WriteEntry<ushort>("SQLServer_Port", SQLServer_Port);
            IConfigurationSerialiser.WriteEntry<bool>("SQLServer_UseWindowsAuthentication", SQLServer_UseWindowsAuthentication);
            IConfigurationSerialiser.WriteEntry<string>("SQLServer_Folder", SQLServer_Folder);
            IConfigurationSerialiser.WriteEntry<string>("SQLServer_Filename", SQLServer_Filename);
            IConfigurationSerialiser.WriteEntry<string>("SQLServer_DatabaseName", SQLServer_DatabaseName);
            IConfigurationSerialiser.WriteEntry<string>("PostgreSQL_IPAddress", PostgreSQL_IPAddress);
            IConfigurationSerialiser.WriteEntry<bool>("PostgreSQL_UseIPv6", PostgreSQL_UseIPv6);
            IConfigurationSerialiser.WriteEntry<ushort>("PostgreSQL_Port", PostgreSQL_Port);
            IConfigurationSerialiser.WriteEntry<bool>("PostgreSQL_UseWindowsAuthentication", PostgreSQL_UseWindowsAuthentication);
            IConfigurationSerialiser.WriteEntry<string>("PostgreSQL_DatabaseName", PostgreSQL_DatabaseName);
            IConfigurationSerialiser.WriteEntry<string>("MySQL_IPAddress", MySQL_IPAddress);
            IConfigurationSerialiser.WriteEntry<bool>("MySQL_UseIPv6", MySQL_UseIPv6);
            IConfigurationSerialiser.WriteEntry<ushort>("MySQL_Port", MySQL_Port);
            IConfigurationSerialiser.WriteEntry<bool>("MySQL_UseWindowsAuthentication", MySQL_UseWindowsAuthentication);
            IConfigurationSerialiser.WriteEntry<string>("MySQL_DatabaseName", MySQL_DatabaseName);
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
