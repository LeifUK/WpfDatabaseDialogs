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

        private string _keyPath = System.Environment.Is64BitOperatingSystem ? @"SOFTWARE\Wow6432Node\WpfFungusApp\DatabaseSettings" : @"SOFTWARE\WpfFungusApp\DatabaseSettings";

        private void _buttonOpenDatabase_Click(object sender, RoutedEventArgs e)
        {
            OpenControls.Wpf.Serialisation.RegistryItemSerialiser registryItemSerialiser = new OpenControls.Wpf.Serialisation.RegistryItemSerialiser(_keyPath);
            OpenControls.Wpf.DatabaseDialogs.Model.DatabaseConfiguration databaseConfiguration = new OpenControls.Wpf.DatabaseDialogs.Model.DatabaseConfiguration(registryItemSerialiser);
            if (registryItemSerialiser.OpenKey())
            {
                databaseConfiguration.Load();
            }

            OpenControls.Wpf.DatabaseDialogs.ViewModel.OpenDatabaseViewModel openDatabaseViewModel = new OpenControls.Wpf.DatabaseDialogs.ViewModel.OpenDatabaseViewModel(databaseConfiguration);
            OpenControls.Wpf.DatabaseDialogs.View.OpenDatabaseView openDatabaseView = 
                new OpenControls.Wpf.DatabaseDialogs.View.OpenDatabaseView(new OpenControls.Wpf.DatabaseDialogs.Model.Encryption());
            openDatabaseView.DataContext = openDatabaseViewModel;
            if (openDatabaseView.ShowDialog() != true)
            {
                return;
            }
            if (!registryItemSerialiser.IsOpen)
            {
                registryItemSerialiser.CreateKey();
            }
            databaseConfiguration.Save();
            registryItemSerialiser.Close();

            try
            {
                if (openDatabaseViewModel.SelectedDatabaseProvider == OpenControls.Wpf.DatabaseDialogs.Model.DatabaseProvider.SQLite)
                {
                    SQLiteDatabase.OpenDatabase(this, openDatabaseViewModel.SQLite_Filename);
                    IDatabaseHost.IConfigurationStore = new DBStore.SQLiteConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.SQLiteSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.SQLiteImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.SQLiteImageStore(IDatabaseHost.Database);
                }
                else if (openDatabaseViewModel.SelectedDatabaseProvider == OpenControls.Wpf.DatabaseDialogs.Model.DatabaseProvider.MicrosoftSQLServer)
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
                else if (openDatabaseViewModel.SelectedDatabaseProvider == OpenControls.Wpf.DatabaseDialogs.Model.DatabaseProvider.PostGreSQL)
                {
                    PostgreSQLDatabase.OpenDatabase(this, openDatabaseViewModel.PostgreSQL_IPAddress, openDatabaseViewModel.PostgreSQL_Port, openDatabaseViewModel.PostgreSQL_UseWindowsAuthentication, openDatabaseViewModel.PostgreSQL_UserName, openDatabaseViewModel.PostgreSQL_Password, openDatabaseViewModel.PostgreSQL_DatabaseName);
                    IDatabaseHost.IConfigurationStore = new DBStore.PostgreSQLConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.PostgreSQLSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.PostgreSQLImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.PostgreSQLImageStore(IDatabaseHost.Database);
                }
                else if (openDatabaseViewModel.SelectedDatabaseProvider == OpenControls.Wpf.DatabaseDialogs.Model.DatabaseProvider.MySQL)
                {
                    MySQLDatabase.OpenDatabase(this, openDatabaseViewModel.MySQL_IPAddress, openDatabaseViewModel.MySQL_Port, openDatabaseViewModel.MySQL_UseWindowsAuthentication, openDatabaseViewModel.MySQL_UserName, openDatabaseViewModel.MySQL_Password, openDatabaseViewModel.MySQL_DatabaseName);
                    IDatabaseHost.IConfigurationStore = new DBStore.MySQLConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.MySQLSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.MySQLImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.MySQLImageStore(IDatabaseHost.Database);
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
            OpenControls.Wpf.Serialisation.RegistryItemSerialiser registryItemSerialiser = new OpenControls.Wpf.Serialisation.RegistryItemSerialiser(_keyPath);
            OpenControls.Wpf.DatabaseDialogs.Model.DatabaseConfiguration databaseConfiguration = new OpenControls.Wpf.DatabaseDialogs.Model.DatabaseConfiguration(registryItemSerialiser);
            if (registryItemSerialiser.OpenKey())
            {
                databaseConfiguration.Load();
            }

            OpenControls.Wpf.DatabaseDialogs.ViewModel.NewDatabaseViewModel newDatabaseViewModel = new OpenControls.Wpf.DatabaseDialogs.ViewModel.NewDatabaseViewModel(databaseConfiguration);
            OpenControls.Wpf.DatabaseDialogs.View.NewDatabaseView newDatabaseView = 
                new OpenControls.Wpf.DatabaseDialogs.View.NewDatabaseView(new OpenControls.Wpf.DatabaseDialogs.Model.Encryption());
            newDatabaseView.DataContext = newDatabaseViewModel;
            if (newDatabaseView.ShowDialog() != true)
            {
                return;
            }
            if (!registryItemSerialiser.IsOpen)
            {
                registryItemSerialiser.CreateKey();
            }
            databaseConfiguration.Save();
            registryItemSerialiser.Close();

            try
            {
                OpenControls.Wpf.DatabaseDialogs.Model.DatabaseProvider databaseProvider = (OpenControls.Wpf.DatabaseDialogs.Model.DatabaseProvider)newDatabaseViewModel.SelectedDatabaseProvider;
                if (databaseProvider == OpenControls.Wpf.DatabaseDialogs.Model.DatabaseProvider.SQLite)
                {
                    SQLiteDatabase.CreateDatabase(this, newDatabaseViewModel.SQLite_Folder, newDatabaseViewModel.SQLite_DatabaseName);
                    IDatabaseHost.IConfigurationStore = new DBStore.SQLiteConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.SQLiteSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.SQLiteImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.SQLiteImageStore(IDatabaseHost.Database);
                }
                else if(databaseProvider == OpenControls.Wpf.DatabaseDialogs.Model.DatabaseProvider.MicrosoftSQLServer)
                {
                    string server =
                        (newDatabaseViewModel.SqlServerInstances.Count > 0) ?
                        newDatabaseViewModel.SqlServerInstances[newDatabaseViewModel.SelectedSqlServerInstance] :
                        null;
                    SQLServerDatabase.CreateDatabase(
                        this,
                        server,
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
                else if (databaseProvider == OpenControls.Wpf.DatabaseDialogs.Model.DatabaseProvider.PostGreSQL)
                {
                    PostgreSQLDatabase.CreateDatabase(this, newDatabaseViewModel.PostgreSQL_IPAddress, newDatabaseViewModel.PostgreSQL_Port, newDatabaseViewModel.PostgreSQL_UseWindowsAuthentication, newDatabaseViewModel.PostgreSQL_UserName, newDatabaseViewModel.PostgreSQL_Password, newDatabaseViewModel.PostgreSQL_DatabaseName);
                    IDatabaseHost.IConfigurationStore = new DBStore.PostgreSQLConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.PostgreSQLSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.PostgreSQLImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.PostgreSQLImageStore(IDatabaseHost.Database);
                }
                else if (databaseProvider == OpenControls.Wpf.DatabaseDialogs.Model.DatabaseProvider.MySQL)
                {
                    MySQLDatabase.CreateDatabase(this, newDatabaseViewModel.MySQL_IPAddress, newDatabaseViewModel.MySQL_Port, newDatabaseViewModel.MySQL_UseWindowsAuthentication, newDatabaseViewModel.MySQL_UserName, newDatabaseViewModel.MySQL_Password, newDatabaseViewModel.MySQL_DatabaseName);
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
