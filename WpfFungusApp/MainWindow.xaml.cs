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
                    SQLServerDatabase.OpenDatabase(this, openDatabaseViewModel.SQLServer_DatabaseName);
                    IDatabaseHost.IConfigurationStore = new DBStore.SQLServerConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.SQLServerSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.SQLServerImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.SQLServerImageStore(IDatabaseHost.Database);
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
            ViewModel.NewDatabaseViewModel newSqlConnectionViewModel = new ViewModel.NewDatabaseViewModel();
            View.NewDatabaseView newSqlConnectionView = new View.NewDatabaseView();
            newSqlConnectionView.DataContext = newSqlConnectionViewModel;
            if (newSqlConnectionView.ShowDialog() != true)
            {
                return;
            }

            try
            {
                DatabaseProvider databaseProvider = (DatabaseProvider)newSqlConnectionViewModel.SelectedDatabaseProvider;
                if (databaseProvider == DatabaseProvider.MicrosoftSQLServer)
                {
                    SQLServerDatabase.CreateDatabase(
                        this,
                        newSqlConnectionViewModel.SqlServerInstances[newSqlConnectionViewModel.SelectedSqlServerInstance],
                        newSqlConnectionViewModel.SQLServer_UserName,
                        newSqlConnectionViewModel.SQLServer_Password,
                        newSqlConnectionViewModel.SQLServer_Folder, 
                        newSqlConnectionViewModel.SQLServer_Filename);
                    IDatabaseHost.IConfigurationStore = new DBStore.SQLServerConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.SQLServerSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.SQLServerImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.SQLServerImageStore(IDatabaseHost.Database);
                }
                else if (databaseProvider == DatabaseProvider.PostGreSQL)
                {
                    PostgreSQLDatabase.CreateDatabase(this, newSqlConnectionViewModel.PostgreSQL_Host, newSqlConnectionViewModel.PostgreSQL_Port, newSqlConnectionViewModel.PostgreSQL_UserName, newSqlConnectionViewModel.PostgreSQL_Password, newSqlConnectionViewModel.PostgreSQL_DatabaseName);

                    IDatabaseHost.IConfigurationStore = new DBStore.PostgreSQLConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.PostgreSQLSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.PostgreSQLImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.PostgreSQLImageStore(IDatabaseHost.Database);
                }
                else if (databaseProvider == DatabaseProvider.SQLite)
                {
                    SQLiteDatabase.CreateDatabase(this, newSqlConnectionViewModel.SQLite_Folder, newSqlConnectionViewModel.SQLite_Filename);
                    IDatabaseHost.IConfigurationStore = new DBStore.SQLiteConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.SQLiteSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.SQLiteImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.SQLiteImageStore(IDatabaseHost.Database);
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
