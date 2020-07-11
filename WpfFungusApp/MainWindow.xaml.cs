using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void Connect()
        {
            //var db = new PetaPoco.Database("Data Source=PetaPoco.sqlite;Version=3;");
            //System.Data.IDbConnection connection = database.Connection;
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

            dialog.Filter = "SQLite Database (*.sqlite)|*.sqlite";
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            var database = new PetaPoco.Database("Data Source=" + dialog.FileName + ";Version=3;", "System.Data.SQLite");
            database.OpenSharedConnection();
            _configurationStore = new DBStore.ConfigurationStore(database);
            _speciesStore = new DBStore.SpeciesStore(database);
            _imagePathsStore = new DBStore.ImagePathsStore(database);
            _imageStore = new DBStore.ImageStore(database);
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

            var database = new PetaPoco.Database("Data Source=" + path + ";Version=3;", "System.Data.SQLite");
            database.OpenSharedConnection();
            _configurationStore = new DBStore.ConfigurationStore(database);
            _configurationStore.CreateTable();
            _configurationStore.Initialise();
            _speciesStore = new DBStore.SpeciesStore(database);
            _speciesStore.CreateTable();
            _imagePathsStore = new DBStore.ImagePathsStore(database);
            _imagePathsStore.CreateTable();
            _imageStore = new DBStore.ImageStore(database);
            _imageStore.CreateTable();
            ShowSpeciesListView(database);
        }
    }
}
