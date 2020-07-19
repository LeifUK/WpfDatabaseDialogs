using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WpfFungusApp.ViewModel
{
    class NewSqlConnectionViewModel : BaseViewModel
    {
        public NewSqlConnectionViewModel()
        {
            SelectedDatabaseProvider = DBStore.DatabaseProvider.SQLite;

            SqlServerInstances = new ObservableCollection<string>();
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

            PostgreSQL_Host = "127.0.0.1";
            PostgreSQL_UseIPv6 = false;
            PostgreSQL_Port = 5432;
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

        private string _sqlServerUserName;
        public string SqlServerUserName
        {
            get
            {
                return _sqlServerUserName;
            }
            set
            {
                _sqlServerUserName = value;
                NotifyPropertyChanged("SqlServerUserName");
            }
        }

        private string _sqlServerPassword;
        public string SqlServerPassword
        {
            get
            {
                return _sqlServerPassword;
            }
            set
            {
                _sqlServerPassword = value;
                NotifyPropertyChanged("SqlServerPassword");
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

        private string _folder;
        public string Folder
        {
            get
            {
                return _folder;
            }
            set
            {
                _folder = value;
                NotifyPropertyChanged("Folder");
            }
        }

        private string _filename;
        public string Filename
        {
            get
            {
                return _filename;
            }
            set
            {
                _filename = value;
                NotifyPropertyChanged("Filename");
            }
        }
    }
}
