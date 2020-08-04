using System;
using System.Collections.ObjectModel;

namespace WpfFungusApp.ViewModel
{
    class OpenDatabaseViewModel : BaseViewModel
    {
        public OpenDatabaseViewModel(IDatabaseConfiguration iDatabaseConfiguration)
        {
            IDatabaseConfiguration = iDatabaseConfiguration;
            // Warning warning
            SelectedDatabaseProvider = DBStore.DatabaseProvider.SQLite;

            SqlServerInstances = new ObservableCollection<string>();
        }

        public readonly IDatabaseConfiguration IDatabaseConfiguration;

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

        public string SQLite_Filename
        {
            get
            {
                return IDatabaseConfiguration.SQLite_Filename;
            }
            set
            {
                IDatabaseConfiguration.SQLite_Filename = value;
                NotifyPropertyChanged("SQLite_Filename");
            }
        }

        public bool SQLServer_UseLocalServer
        {
            get
            {
                return IDatabaseConfiguration.SQLServer_UseLocalServer;
            }
            set
            {
                IDatabaseConfiguration.SQLServer_UseLocalServer = value;
                NotifyPropertyChanged("SQLServer_UseLocalServer");
            }
        }

        public int SelectedSqlServerInstance
        {
            get
            {
                return IDatabaseConfiguration.SelectedSqlServerInstance;
            }
            set
            {
                IDatabaseConfiguration.SelectedSqlServerInstance = value;
                NotifyPropertyChanged("SelectedSqlServerInstance");
            }
        }

        public string SQLServer_IPAddress
        {
            get
            {
                return IDatabaseConfiguration.SQLServer_IPAddress;
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
                        IDatabaseConfiguration.SQLServer_IPAddress = text;
                    }
                    else
                    {
                        IDatabaseConfiguration.SQLServer_IPAddress = ipAddress.ToString();
                    }
                }
                catch
                {

                }
                NotifyPropertyChanged("SQLServer_IPAddress");
            }
        }

        public bool SQLServer_UseIPv6
        {
            get
            {
                return IDatabaseConfiguration.SQLServer_UseIPv6;
            }
            set
            {
                if (IDatabaseConfiguration.SQLServer_UseIPv6 != value)
                {
                    IDatabaseConfiguration.SQLServer_UseIPv6 = value;
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

        public ushort SQLServer_Port
        {
            get
            {
                return IDatabaseConfiguration.SQLServer_Port;
            }
            set
            {
                IDatabaseConfiguration.SQLServer_Port = value;
                NotifyPropertyChanged("SQLServer_Port");
            }
        }

        public bool SQLServer_UseWindowsAuthentication
        {
            get
            {
                return IDatabaseConfiguration.SQLServer_UseWindowsAuthentication;
            }
            set
            {
                IDatabaseConfiguration.SQLServer_UseWindowsAuthentication = value;
                NotifyPropertyChanged("SQLServer_UseWindowsAuthentication");
            }
        }
        
        public string SQLServer_UserName
        {
            get
            {
                return IDatabaseConfiguration.SQLServer_UserName;
            }
            set
            {
                IDatabaseConfiguration.SQLServer_UserName = value;
                NotifyPropertyChanged("SQLServer_UserName");
            }
        }

        public string SQLServer_Password
        {
            get
            {
                return IDatabaseConfiguration.SQLServer_Password;
            }
            set
            {
                IDatabaseConfiguration.SQLServer_Password = value;
                NotifyPropertyChanged("SQLServer_Password");
            }
        }

        public string SQLServer_Folder
        {
            get
            {
                return IDatabaseConfiguration.SQLServer_Folder;
            }
            set
            {
                IDatabaseConfiguration.SQLServer_Folder = value;
                NotifyPropertyChanged("SQLServer_Folder");
            }
        }

        public string SQLServer_DatabaseName
        {
            get
            {
                return IDatabaseConfiguration.SQLServer_DatabaseName;
            }
            set
            {
                IDatabaseConfiguration.SQLServer_DatabaseName = value;
                NotifyPropertyChanged("SQLServer_DatabaseName");
            }
        }

        public string PostgreSQL_Host
        {
            get
            {
                return IDatabaseConfiguration.PostgreSQL_Host;
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
                        IDatabaseConfiguration.PostgreSQL_Host = text;
                    }
                    else
                    {
                        IDatabaseConfiguration.PostgreSQL_Host = ipAddress.ToString();
                    }
                }
                catch
                {

                }
                NotifyPropertyChanged("PostgreSQL_Host");
            }
        }

        public bool PostgreSQL_UseIPv6
        {
            get
            {
                return IDatabaseConfiguration.PostgreSQL_UseIPv6;
            }
            set
            {
                if (IDatabaseConfiguration.PostgreSQL_UseIPv6 != value)
                {
                    IDatabaseConfiguration.PostgreSQL_UseIPv6 = value;
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

        public ushort PostgreSQL_Port
        {
            get
            {
                return IDatabaseConfiguration.PostgreSQL_Port;
            }
            set
            {
                IDatabaseConfiguration.PostgreSQL_Port = value;
                NotifyPropertyChanged("PostgreSQL_Port");
            }
        }

        public bool PostgreSQL_UseWindowsAuthentication
        {
            get
            {
                return IDatabaseConfiguration.PostgreSQL_UseWindowsAuthentication;
            }
            set
            {
                IDatabaseConfiguration.PostgreSQL_UseWindowsAuthentication = value;
                NotifyPropertyChanged("PostgreSQL_UseWindowsAuthentication");
            }
        }

        public string PostgreSQL_UserName
        {
            get
            {
                return IDatabaseConfiguration.PostgreSQL_UserName;
            }
            set
            {
                IDatabaseConfiguration.PostgreSQL_UserName = value;
                NotifyPropertyChanged("PostgreSQL_UserName");
            }
        }

        public string PostgreSQL_Password
        {
            get
            {
                return IDatabaseConfiguration.PostgreSQL_Password;
            }
            set
            {
                IDatabaseConfiguration.PostgreSQL_Password = value;
                NotifyPropertyChanged("PostgreSQL_Password");
            }
        }

        public string PostgreSQL_DatabaseName
        {
            get
            {
                return IDatabaseConfiguration.PostgreSQL_DatabaseName;
            }
            set
            {
                IDatabaseConfiguration.PostgreSQL_DatabaseName = value;
                NotifyPropertyChanged("PostgreSQL_DatabaseName");
            }
        }

        public bool MySQL_UseWindowsAuthentication
        {
            get
            {
                return IDatabaseConfiguration.MySQL_UseWindowsAuthentication;
            }
            set
            {
                IDatabaseConfiguration.MySQL_UseWindowsAuthentication = value;
                NotifyPropertyChanged("MySQL_UseWindowsAuthentication");
            }
        }

        public string MySQL_UserName
        {
            get
            {
                return IDatabaseConfiguration.MySQL_UserName;
            }
            set
            {
                IDatabaseConfiguration.MySQL_UserName = value;
                NotifyPropertyChanged("MySQL_UserName");
            }
        }

        public string MySQL_Password
        {
            get
            {
                return IDatabaseConfiguration.MySQL_Password;
            }
            set
            {
                IDatabaseConfiguration.MySQL_Password = value;
                NotifyPropertyChanged("MySQL_Password");
            }
        }
    }
}
