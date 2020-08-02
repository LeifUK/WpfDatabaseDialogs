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
            ViewModel.OpenDatabaseViewModel openDatabaseViewModelModel = new ViewModel.OpenDatabaseViewModel();
            View.OpenDatabaseView newSqlConnectionView = new View.OpenDatabaseView();
            newSqlConnectionView.DataContext = openDatabaseViewModelModel;
            if (newSqlConnectionView.ShowDialog() != true)
            {
                return;
            }

            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();

            dialog.Filter = "SQLite Database (*.sqlite)|*.sqlite|Microsoft SQL Server Database(*.mdf) | *.mdf";
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            try
            {
                if (dialog.FilterIndex == 1)
                {
                    SQLiteDatabase.OpenDatabase(this, dialog.FileName);
                    IDatabaseHost.IConfigurationStore = new DBStore.SQLiteConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.SQLiteSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.SQLiteImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.SQLiteImageStore(IDatabaseHost.Database);
                }
                else if (dialog.FilterIndex == 2)
                {
                    SQLServerDatabase.OpenDatabase(this, dialog.FileName);
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
                if (databaseProvider == DatabaseProvider.MicrosoftSqlServer)
                {
                    SQLServerDatabase.CreateDatabase(
                        this,
                        newSqlConnectionViewModel.SqlServerInstances[newSqlConnectionViewModel.SelectedSqlServerInstance],
                        newSqlConnectionViewModel.SQLServer_UserName,
                        newSqlConnectionViewModel.SQLServer_Password,
                        newSqlConnectionViewModel.SQLServer_Folder, 
                        newSqlConnectionViewModel.SQLServer_Filename);
                    IDatabaseHost.IConfigurationStore = new DBStore.PostgreSQLConfigurationStore(IDatabaseHost.Database);
                    IDatabaseHost.ISpeciesStore = new DBStore.PostgreSQLSpeciesStore(IDatabaseHost.Database);
                    IDatabaseHost.IImagePathsStore = new DBStore.PostgreSQLImagePathsStore(IDatabaseHost.Database);
                    IDatabaseHost.IImageStore = new DBStore.PostgreSQLImageStore(IDatabaseHost.Database);
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
