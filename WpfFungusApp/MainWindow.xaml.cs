using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfFungusApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private DBStore.ConfigurationStore _configurationStore;
        private DBStore.SpeciesStore _speciesStore;
        private DBStore.ImagePathsStore _imagePathsStore;
        private DBStore.ImageStore _imageStore;

        private void ShowSpeciesListView(PetaPoco.Database database)
        {
            View.SpeciesListView speciesListView = new View.SpeciesListView();
            ViewModel.SpeciesListViewModel speciesListViewModel = new ViewModel.SpeciesListViewModel(
                database,
                _configurationStore,
                _speciesStore,
                _imagePathsStore,
                _imageStore);
            speciesListView.DataContext = speciesListViewModel;
            speciesListViewModel.Load();
            speciesListView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            speciesListView.Owner = this;
            speciesListView.ShowDialog();
            speciesListView._datagrid.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void _buttonOpenDatabase_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();

            dialog.Filter = "SQLite Database (*.sqlite)|*.sqlite|Microsoft SQL Server Database(*.mdf) | *.mdf";
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            PetaPoco.Database database = null;
            if (dialog.FilterIndex == 1)
            {
                database = new PetaPoco.Database("Data Source=" + dialog.FileName + ";Version=3;", "System.Data.SQLite");
            }
            else if (dialog.FilterIndex == 2)
            {
                string dbName = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);
                database = new PetaPoco.Database(@"Data Source=.\SQLEXPRESS; Integrated security = SSPI; database = " + dbName, "System.Data.SqlClient");
            }
            else
            {
                throw new Exception("Unsupported database type");
            }
                
            database.OpenSharedConnection();
            _configurationStore = new DBStore.SQLiteConfigurationStore(database);
            _speciesStore = new DBStore.SQLiteSpeciesStore(database);
            _imagePathsStore = new DBStore.SQLiteImagePathsStore(database);
            _imageStore = new DBStore.SQLiteImageStore(database);
            ShowSpeciesListView(database);
        }

        private void _buttonNewDatabase_Click(object sender, RoutedEventArgs e)
        {
            View.NewDatabaseView newDatabaseView = new View.NewDatabaseView();
            ViewModel.NewDatabaseViewModel newDatabaseViewModel = new ViewModel.NewDatabaseViewModel();
            newDatabaseView.DataContext = newDatabaseViewModel;

            if (newDatabaseView.ShowDialog() != true)
            {
                return;
            }

            string path = System.IO.Path.Combine(newDatabaseViewModel.Folder, newDatabaseViewModel.Name + ".sqlite");

            PetaPoco.Database database = null;
            if (newDatabaseViewModel.SelectedDatabaseProvider == 0)
            {
                database = new PetaPoco.Database("Data Source=" + path + ";Version=3;", "System.Data.SQLite");
                database.OpenSharedConnection();

                _configurationStore = new DBStore.SQLiteConfigurationStore(database);
                _speciesStore = new DBStore.SQLiteSpeciesStore(database);
                _imagePathsStore = new DBStore.SQLiteImagePathsStore(database);
                _imageStore = new DBStore.SQLiteImageStore(database);
            }
            else if (newDatabaseViewModel.SelectedDatabaseProvider == 1)
            {
                string dbName = newDatabaseViewModel.Name;
                string filename = System.IO.Path.Combine(newDatabaseViewModel.Folder, dbName);

                // Connect to the master DB to create the requested database

                database = new PetaPoco.Database(@"Data Source=.\SQLEXPRESS; Integrated security = SSPI; database = master", "System.Data.SqlClient");
                database.OpenSharedConnection();
                database.Execute("CREATE DATABASE " + dbName + " ON PRIMARY (Name=" + dbName + ", filename = \"" + filename + ".mdf\")log on (name=" + dbName + "_log, filename=\"" + filename + ".ldf\")");
                database.CloseSharedConnection();

                // Connect to the new database

                database = new PetaPoco.Database(@"Data Source=.\SQLEXPRESS; Integrated security = SSPI; database = " + dbName, "System.Data.SqlClient");
                database.OpenSharedConnection();

                _configurationStore = new DBStore.SQLServerConfigurationStore(database);
                _speciesStore = new DBStore.SQLServerSpeciesStore(database);
                _imagePathsStore = new DBStore.SQLServerImagePathsStore(database);
                _imageStore = new DBStore.SQLServerImageStore(database);
            }
            else
            {
                throw new Exception("Unsupported database type");
            }

            _configurationStore.CreateTable();
            _configurationStore.Initialise();
            _speciesStore.CreateTable();
            _imagePathsStore.CreateTable();
            _imageStore.CreateTable();
            ShowSpeciesListView(database);
        }
    }
}
