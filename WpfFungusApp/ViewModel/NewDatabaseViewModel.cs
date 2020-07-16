using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Policy;

namespace WpfFungusApp.ViewModel
{
    internal class NewDatabaseViewModel : BaseViewModel
    {
        public NewDatabaseViewModel()
        {
            DatabaseProviders = new ObservableCollection<KeyValuePair<string, int>>();
            DatabaseProviders.Add(new KeyValuePair<string, int>("SQLite", (int)DBStore.DatabaseProvider.SQLite));
            DatabaseProviders.Add(new KeyValuePair<string, int>("Microsoft SQL Server", (int)DBStore.DatabaseProvider.MicrosoftSqlServer));

            SelectedDatabaseProvider = (int)DBStore.DatabaseProvider.SQLite;
        }

        private ObservableCollection<KeyValuePair<string, int>> _databaseProviders;
        public ObservableCollection<KeyValuePair<string, int>> DatabaseProviders
        {
            get
            {
                return _databaseProviders;
            }
            set
            {
                _databaseProviders = value;
                NotifyPropertyChanged("DatabaseProviders");
            }
        }

        private int _selectedDatabaseProvider;
        public int SelectedDatabaseProvider
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

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
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
    }
}
