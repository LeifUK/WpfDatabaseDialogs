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
