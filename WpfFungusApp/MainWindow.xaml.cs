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
                }
                else if (dialog.FilterIndex == 2)
                {
                    SQLServerDatabase.OpenDatabase(this, dialog.FileName);
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
            View.NewDatabaseView newDatabaseView = new View.NewDatabaseView();
            ViewModel.NewDatabaseViewModel newDatabaseViewModel = new ViewModel.NewDatabaseViewModel();
            newDatabaseView.DataContext = newDatabaseViewModel;

            if (newDatabaseView.ShowDialog() != true)
            {
                return;
            }

            try
            {
                DatabaseType databaseType = (DatabaseType)newDatabaseViewModel.SelectedDatabaseProvider;
                if (databaseType == DatabaseType.MicrosoftSqlServer)
                {
                    SQLServerDatabase.NewDatabaseView(this, newDatabaseViewModel.Folder, newDatabaseViewModel.Name);
                }
                else if (databaseType == DatabaseType.SQLite)
                {
                    SQLiteDatabase.NewDatabaseView(this, newDatabaseViewModel.Folder, newDatabaseViewModel.Name);
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
    }
}
