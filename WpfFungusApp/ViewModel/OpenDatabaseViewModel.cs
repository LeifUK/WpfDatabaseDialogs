﻿using System;
using System.Collections.ObjectModel;

namespace WpfFungusApp.ViewModel
{
    class OpenDatabaseViewModel : BaseViewModel
    {
        public OpenDatabaseViewModel()
        {
            SelectedDatabaseProvider = DBStore.DatabaseProvider.SQLite;

            SqlServerInstances = new ObservableCollection<string>();
            SQLServer_UseLocalServer = true;
            SQLServer_IPAddress = "127.0.0.1";
            SQLServer_UseIPv6 = false;
            SQLServer_Port = 1433;
            SQLServer_UseWindowsAuthentication = true;

            PostgreSQL_Host = "127.0.0.1";
            PostgreSQL_UseIPv6 = false;
            PostgreSQL_Port = 5432;
            PostgreSQL_UseWindowsAuthentication = true;

            MySQL_UseWindowsAuthentication = true;
        }

        public void Refresh()
        {
            System.Data.Sql.SqlDataSourceEnumerator instance = System.Data.Sql.SqlDataSourceEnumerator.Instance;
            System.Data.DataTable dataTable = instance.GetDataSources();

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                foreach (System.Data.DataColumn col in dataTable.Columns)
                {
                    Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
                }

                SqlServerInstances.Add(row[0] + "\\" + row[1]);
            }

            SelectedSqlServerInstance = 0;
        }

        private DBStore.DatabaseProvider _selectedDatabaseProvider;
        public DBStore.DatabaseProvider SelectedDatabaseProvider
        {
            get
            {
                return _selectedDatabaseProvider;
            }
            set
            {
                _selectedDatabaseProvider = value;
                NotifyPropertyChanged("SelectedDatabaseProvider");
            }
        }

        private ObservableCollection<string> _sqlServerInstances;
        public ObservableCollection<string> SqlServerInstances
        {
            get
            {
                return _sqlServerInstances;
            }
            set
            {
                _sqlServerInstances = value;
                NotifyPropertyChanged("SqlServerInstances");
            }
        }

        private string _sqlite_Filename;
        public string SQLite_Filename
        {
            get
            {
                return _sqlite_Filename;
            }
            set
            {
                _sqlite_Filename = value;
                NotifyPropertyChanged("SQLite_Filename");
            }
        }

        private bool _sqlServer_LocalServer;
        public bool SQLServer_UseLocalServer
        {
            get
            {
                return _sqlServer_LocalServer;
            }
            set
            {
                _sqlServer_LocalServer = value;
                NotifyPropertyChanged("SQLServer_UseLocalServer");
            }
        }

        private int _selectedSqlServerInstance;
        public int SelectedSqlServerInstance
        {
            get
            {
                return _selectedSqlServerInstance;
            }
            set
            {
                _selectedSqlServerInstance = value;
                NotifyPropertyChanged("SelectedSqlServerInstance");
            }
        }

        private string _sqlServer_IPAddress;
        public string SQLServer_IPAddress
        {
            get
            {
                return _sqlServer_IPAddress;
            }
            set
            {
                try
                {
                    System.Net.IPAddress ipAddress = System.Net.IPAddress.Parse(value);
                    if (SQLServer_UseIPv6)
                    {
                        byte[] bytes = ipAddress.GetAddressBytes();
                        bool first = true;
                        string text = "";
                        for (int index = 0; index < 16; index += 2)
                        {
                            if (!first)
                            {
                                text += ":";
                            }
                            short shortVal = (short)(bytes[index + 1] + (bytes[index] << 8));
                            first = false;
                            text += shortVal.ToString("X");
                        }
                        _sqlServer_IPAddress = text;
                    }
                    else
                    {
                        _sqlServer_IPAddress = ipAddress.ToString();
                    }
                }
                catch
                {

                }
                NotifyPropertyChanged("SQLServer_IPAddress");
            }
        }

        private bool _sqlServer_UseIPv6;
        public bool SQLServer_UseIPv6
        {
            get
            {
                return _sqlServer_UseIPv6;
            }
            set
            {
                if (_sqlServer_UseIPv6 != value)
                {
                    _sqlServer_UseIPv6 = value;
                    if (value)
                    {
                        SQLServer_IPAddress = "0:0:0:0:0:0:0:1";
                    }
                    else
                    {
                        SQLServer_IPAddress = "127.0.0.1";
                    }
                }
                NotifyPropertyChanged("SQLServer_UseIPv6");
            }
        }

        private ushort _sqlServer_Port;
        public ushort SQLServer_Port
        {
            get
            {
                return _sqlServer_Port;
            }
            set
            {
                _sqlServer_Port = value;
                NotifyPropertyChanged("SQLServer_Port");
            }
        }

        private bool _sqlServer_UseWindowsAuthentication;
        public bool SQLServer_UseWindowsAuthentication
        {
            get
            {
                return _sqlServer_UseWindowsAuthentication;
            }
            set
            {
                _sqlServer_UseWindowsAuthentication = value;
                NotifyPropertyChanged("SQLServer_UseWindowsAuthentication");
            }
        }
        
        private string _sqlServer_UserName;
        public string SQLServer_UserName
        {
            get
            {
                return _sqlServer_UserName;
            }
            set
            {
                _sqlServer_UserName = value;
                NotifyPropertyChanged("SQLServer_UserName");
            }
        }

        private string _sqlServer_Password;
        public string SQLServer_Password
        {
            get
            {
                return _sqlServer_Password;
            }
            set
            {
                _sqlServer_Password = value;
                NotifyPropertyChanged("SQLServer_Password");
            }
        }

        private string _sqlServer_Folder;
        public string SQLServer_Folder
        {
            get
            {
                return _sqlServer_Folder;
            }
            set
            {
                _sqlServer_Folder = value;
                NotifyPropertyChanged("SQLServer_Folder");
            }
        }

        private string _sqlServer_DatabaseName;
        public string SQLServer_DatabaseName
        {
            get
            {
                return _sqlServer_DatabaseName;
            }
            set
            {
                _sqlServer_DatabaseName = value;
                NotifyPropertyChanged("SQLServer_DatabaseName");
            }
        }

        private string _postgreSQL_Host;
        public string PostgreSQL_Host
        {
            get
            {
                return _postgreSQL_Host;
            }
            set
            {
                try
                {
                    System.Net.IPAddress ipAddress = System.Net.IPAddress.Parse(value);
                    if (PostgreSQL_UseIPv6)
                    {
                        byte[] bytes = ipAddress.GetAddressBytes();
                        bool first = true;
                        string text = "";
                        for (int index = 0; index < 16; index += 2)
                        {
                            if (!first)
                            {
                                text += ":";
                            }
                            short shortVal = (short)(bytes[index + 1] + (bytes[index] << 8));
                            first = false;
                            text += shortVal.ToString("X");
                        }
                        _postgreSQL_Host = text;
                    }
                    else
                    {
                        _postgreSQL_Host = ipAddress.ToString();
                    }
                }
                catch
                {

                }
                NotifyPropertyChanged("PostgreSQL_Host");
            }
        }

        private bool _postgreSQL_UseIPv6;
        public bool PostgreSQL_UseIPv6
        {
            get
            {
                return _postgreSQL_UseIPv6;
            }
            set
            {
                if (_postgreSQL_UseIPv6 != value)
                {
                    _postgreSQL_UseIPv6 = value;
                    if (value)
                    {
                        PostgreSQL_Host = "0:0:0:0:0:0:0:1";
                    }
                    else
                    {
                        PostgreSQL_Host = "127.0.0.1";
                    }
                }
                NotifyPropertyChanged("PostgreSQL_UseIPv6");
            }
        }

        private ushort _postgreSQL_Port;
        public ushort PostgreSQL_Port
        {
            get
            {
                return _postgreSQL_Port;
            }
            set
            {
                _postgreSQL_Port = value;
                NotifyPropertyChanged("PostgreSQL_Port");
            }
        }

        private bool _postgreSQL_UseWindowsAuthentication;
        public bool PostgreSQL_UseWindowsAuthentication
        {
            get
            {
                return _postgreSQL_UseWindowsAuthentication;
            }
            set
            {
                _postgreSQL_UseWindowsAuthentication = value;
                NotifyPropertyChanged("PostgreSQL_UseWindowsAuthentication");
            }
        }

        private string _postgreSQL_UserName;
        public string PostgreSQL_UserName
        {
            get
            {
                return _postgreSQL_UserName;
            }
            set
            {
                _postgreSQL_UserName = value;
                NotifyPropertyChanged("PostgreSQL_UserName");
            }
        }

        private string _postgreSQL_Password;
        public string PostgreSQL_Password
        {
            get
            {
                return _postgreSQL_Password;
            }
            set
            {
                _postgreSQL_Password = value;
                NotifyPropertyChanged("PostgreSQL_Password");
            }
        }

        private string _postgreSQL_DatabaseName;
        public string PostgreSQL_DatabaseName
        {
            get
            {
                return _postgreSQL_DatabaseName;
            }
            set
            {
                _postgreSQL_DatabaseName = value;
                NotifyPropertyChanged("PostgreSQL_DatabaseName");
            }
        }

        private bool _mySQL_UseWindowsAuthentication;
        public bool MySQL_UseWindowsAuthentication
        {
            get
            {
                return _mySQL_UseWindowsAuthentication;
            }
            set
            {
                _mySQL_UseWindowsAuthentication = value;
                NotifyPropertyChanged("MySQL_UseWindowsAuthentication");
            }
        }

        private string _mySQL_UserName;
        public string MySQL_UserName
        {
            get
            {
                return _mySQL_UserName;
            }
            set
            {
                _mySQL_UserName = value;
                NotifyPropertyChanged("MySQL_UserName");
            }
        }

        private string _mySQL_Password;
        public string MySQL_Password
        {
            get
            {
                return _mySQL_Password;
            }
            set
            {
                _mySQL_Password = value;
                NotifyPropertyChanged("MySQL_Password");
            }
        }
    }
}
