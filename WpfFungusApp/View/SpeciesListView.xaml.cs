using System.Linq;
using System.Windows;
using System.Collections.Generic;

namespace WpfFungusApp.View
{
    /// <summary>
    /// Interaction logic for SpeciesListView.xaml
    /// </summary>
    public partial class SpeciesListView : Window
    {
        public SpeciesListView()
        {
            InitializeComponent();
            _datagrid.IsReadOnly = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _datagrid.Columns[0].Visibility = Visibility.Collapsed;
            //for (int index = 0; index < Database.SpeciesTable.FieldDescriptors.Count; ++index)
            //{
            //    _datagrid.Columns[index].Header = Database.SpeciesTable.FieldDescriptors[index].DisplayName;
            //}
        }


        //private void LoadImages(DBStore.IDatabaseHost iDatabaseHost, DBObject.Species species)
        //{
        //    Dictionary<long, string> paths = DBStore.DatabaseHelpers.LoadImagePaths(iDatabaseHost.IImagePathsStore);

        //    species.Images = iDatabaseHost.IImageStore.LoadImages(species.id, paths);
        //}


        private void _buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_datagrid.SelectedIndex < 0)
            {
                return;
            }

            int selectedIndex = _datagrid.SelectedIndex;
            ViewModel.SpeciesListViewModel speciesListViewModel = DataContext as ViewModel.SpeciesListViewModel;
            DBObject.Species species = speciesListViewModel.SpeciesCollection[_datagrid.SelectedIndex] as DBObject.Species;
            // Edit a clone of the species
            DBObject.Species editedSpecies = species.Clone();
            speciesListViewModel.LoadImages(species);
            speciesListViewModel.LoadImages(editedSpecies);

            View.SpeciesView speciesView = new SpeciesView();
            ViewModel.SpeciesViewModel speciesViewModel = new ViewModel.SpeciesViewModel(speciesListViewModel.IDatabaseHost.IConfigurationStore, editedSpecies);
            speciesView.DataContext = speciesViewModel;
            speciesView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            speciesView.Owner = this;
            if (speciesView.ShowDialog() == false)
            {
                return;
            }

            speciesListViewModel.UpdateSpecies(species, editedSpecies);
            _datagrid.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void _buttonNew_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SpeciesListViewModel speciesListViewModel = DataContext as ViewModel.SpeciesListViewModel;
            DBObject.Species species = new DBObject.Species();

            View.SpeciesView speciesView = new SpeciesView();
            ViewModel.SpeciesViewModel speciesViewModel = new ViewModel.SpeciesViewModel(speciesListViewModel.IDatabaseHost.IConfigurationStore, species);
            speciesView.DataContext = speciesViewModel;
            speciesView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            speciesView.Owner = this;
            if (speciesView.ShowDialog() == false)
            {
                return;
            }

            // Save everything

            speciesListViewModel.InsertSpecies(species);
            _datagrid.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void _buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_datagrid.SelectedIndex < 0)
            {
                return;
            }

            int selectedIndex = _datagrid.SelectedIndex;

            ViewModel.SpeciesListViewModel speciesListViewModel = DataContext as ViewModel.SpeciesListViewModel;
            speciesListViewModel.DeleteSpecies(selectedIndex);

            _datagrid.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void _buttonConfigure_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SpeciesListViewModel speciesListViewModel = DataContext as ViewModel.SpeciesListViewModel;
            View.ConfigurationView configurationView = new ConfigurationView();
            ViewModel.ConfigurationViewModel configurationViewModel = new ViewModel.ConfigurationViewModel(speciesListViewModel.IDatabaseHost);
            configurationView.DataContext = configurationViewModel;
            configurationView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            configurationView.Owner = this;
            configurationView.ShowDialog();
        }

        private void _buttonCloseDB_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void _buttonExportHTML_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
