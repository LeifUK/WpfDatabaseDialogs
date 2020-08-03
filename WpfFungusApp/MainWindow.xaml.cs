using PetaPoco;
using System;
using System.Windows;
using WpfFungusApp.DBStore;

namespace WpfFungusApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, DBStore.IDatabaseHost
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        IDatabaseHost IDatabaseHost { get { return this; } }
        Database IDatabaseHost.Database { get; set; }
        IConfigurationStore IDatabaseHost.IConfigurationStore { get; set; }
        ISpeciesStore IDatabaseHost.ISpeciesStore { get; set; }
        IImagePathsStore IDatabaseHost.IImagePathsStore { get; set; }
        IImageStore IDatabaseHost.IImageStore { get; set; }

        private void ShowSpeciesListView(PetaPoco.Database database)
        {
            View.SpeciesListView speciesListView = new View.SpeciesListView();
            ViewModel.SpeciesListViewModel speciesListViewModel = new ViewModel.SpeciesListViewModel(this);
            speciesListView.DataContext = speciesListViewModel;
            speciesListViewModel.Load();
            speciesListView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            speciesListView.Owner = this;
            speciesListView.ShowDialog();
            speciesListView._datagrid.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void _buttonOpenDatabase_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenDatabaseViewModel openDatabaseViewModel = new ViewModel.OpenDatabaseViewModel();
            View.OpenDatabaseView openDatabaseView = new View.OpenDatabaseView();
            openDatabaseView.DataContext = openDatabaseViewModel;
            if (openDatabaseView.ShowDialog() != true)
            {
                return;
            }

            try
            {
                if (openDatabaseViewModel.SelectedDatabaseProvider == DatabaseProvider.SQLite)
                {
                    SQLiteDatabase.OpenDatabase(this, openDatabaseViewModel.SQLite_Filename);
                    IDatabaseHost.IConfigurationStore = new DBStore.SQLiteConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.SQLiteSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.SQLiteImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.SQLiteImageStore(IDatabaseHost.Database);
                }
                else if (openDatabaseViewModel.SelectedDatabaseProvider == DatabaseProvider.MicrosoftSQLServer)
                {
                    string server = 
                        (openDatabaseViewModel.SqlServerInstances.Count > 0) ?
                        openDatabaseViewModel.SqlServerInstances[openDatabaseViewModel.SelectedSqlServerInstance] :
                        null;
                    SQLServerDatabase.OpenDatabase(
                        this,
                        openDatabaseViewModel.SQLServer_UseLocalServer,
                        server,
                        openDatabaseViewModel.SQLServer_IPAddress,
                        openDatabaseViewModel.SQLServer_Port,
                        openDatabaseViewModel.SQLServer_UseWindowsAuthentication,
                        openDatabaseViewModel.SQLServer_UserName,
                        openDatabaseViewModel.SQLServer_Password,
                        openDatabaseViewModel.SQLServer_DatabaseName);
                    IDatabaseHost.IConfigurationStore = new DBStore.SQLServerConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.SQLServerSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.SQLServerImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.SQLServerImageStore(IDatabaseHost.Database);
                }
                else if (openDatabaseViewModel.SelectedDatabaseProvider == DatabaseProvider.PostGreSQL)
                {
                    PostgreSQLDatabase.OpenDatabase(this, openDatabaseViewModel.PostgreSQL_Host, openDatabaseViewModel.PostgreSQL_Port, openDatabaseViewModel.PostgreSQL_UseWindowsAuthentication, openDatabaseViewModel.PostgreSQL_UserName, openDatabaseViewModel.PostgreSQL_Password, openDatabaseViewModel.PostgreSQL_DatabaseName);
                    IDatabaseHost.IConfigurationStore = new DBStore.PostgreSQLConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.PostgreSQLSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.PostgreSQLImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.PostgreSQLImageStore(IDatabaseHost.Database);
                }
                else
                {
                    throw new Exception("Unsupported database type");
                }
            }
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message);
                return;
            }

            ShowSpeciesListView(IDatabaseHost.Database);
        }

        private void _buttonNewDatabase_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NewDatabaseViewModel newDatabaseViewModel = new ViewModel.NewDatabaseViewModel();
            View.NewDatabaseView newDatabaseView = new View.NewDatabaseView();
            newDatabaseView.DataContext = newDatabaseViewModel;
            if (newDatabaseView.ShowDialog() != true)
            {
                return;
            }

            try
            {
                DatabaseProvider databaseProvider = (DatabaseProvider)newDatabaseViewModel.SelectedDatabaseProvider;
                if (databaseProvider == DatabaseProvider.MicrosoftSQLServer)
                {
                    SQLServerDatabase.CreateDatabase(
                        this,
                        newDatabaseViewModel.SqlServerInstances[newDatabaseViewModel.SelectedSqlServerInstance],
                        newDatabaseViewModel.SQLServer_UseWindowsAuthentication,
                        newDatabaseViewModel.SQLServer_UserName,
                        newDatabaseViewModel.SQLServer_Password,
                        newDatabaseViewModel.SQLServer_Folder, 
                        newDatabaseViewModel.SQLServer_Filename);
                    IDatabaseHost.IConfigurationStore = new DBStore.SQLServerConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.SQLServerSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.SQLServerImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.SQLServerImageStore(IDatabaseHost.Database);
                }
                else if (databaseProvider == DatabaseProvider.PostGreSQL)
                {
                    PostgreSQLDatabase.CreateDatabase(this, newDatabaseViewModel.PostgreSQL_Host, newDatabaseViewModel.PostgreSQL_Port, newDatabaseViewModel.PostgreSQL_UseWindowsAuthentication, newDatabaseViewModel.PostgreSQL_UserName, newDatabaseViewModel.PostgreSQL_Password, newDatabaseViewModel.PostgreSQL_DatabaseName);
                    IDatabaseHost.IConfigurationStore = new DBStore.PostgreSQLConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.PostgreSQLSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.PostgreSQLImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.PostgreSQLImageStore(IDatabaseHost.Database);
                }
                else if (databaseProvider == DatabaseProvider.SQLite)
                {
                    SQLiteDatabase.CreateDatabase(this, newDatabaseViewModel.SQLite_Folder, newDatabaseViewModel.SQLite_Filename);
                    IDatabaseHost.IConfigurationStore = new DBStore.SQLiteConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.SQLiteSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.SQLiteImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.SQLiteImageStore(IDatabaseHost.Database);
                }
                else if (databaseProvider == DatabaseProvider.MySQL)
                {
                    MySQLDatabase.CreateDatabase(this, newDatabaseViewModel.MySQL_Host, newDatabaseViewModel.MySQL_Port, newDatabaseViewModel.MySQL_UseWindowsAuthentication, newDatabaseViewModel.MySQL_UserName, newDatabaseViewModel.MySQL_Password, newDatabaseViewModel.MySQL_DatabaseName);
                    IDatabaseHost.IConfigurationStore = new DBStore.MySQLConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.MySQLSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.MySQLImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.MySQLImageStore(IDatabaseHost.Database);
                }
                else
                {
                    throw new Exception("Unsupported database type");
                }

                IDatabaseHost.IConfigurationStore.CreateTable();
                IDatabaseHost.IConfigurationStore.Initialise();
                IDatabaseHost.ISpeciesStore.CreateTable();
                IDatabaseHost.IImagePathsStore.CreateTable();
                IDatabaseHost.IImageStore.CreateTable();
            }
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message);
                return;
            }

            ShowSpeciesListView(IDatabaseHost.Database);
        }
    }
}
