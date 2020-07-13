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
            DBStore.DatabaseHelpers.LoadImages(speciesListViewModel.Database, speciesListViewModel.IImagePathsStore, editedSpecies);

            View.SpeciesView speciesView = new SpeciesView();
            ViewModel.SpeciesViewModel speciesViewModel = new ViewModel.SpeciesViewModel(speciesListViewModel.IConfigurationStore, editedSpecies);
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
            ViewModel.SpeciesViewModel speciesViewModel = new ViewModel.SpeciesViewModel(speciesListViewModel.IConfigurationStore, species);
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
            ViewModel.ConfigurationViewModel configurationViewModel = new ViewModel.ConfigurationViewModel(
                speciesListViewModel.IConfigurationStore,
                speciesListViewModel.IImagePathsStore,
                speciesListViewModel.IImageStore);
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
            ViewModel.SpeciesListViewModel speciesListViewModel = DataContext as ViewModel.SpeciesListViewModel;

            string exportFolder = speciesListViewModel.IConfigurationStore.ExportFolder;
            bool overwriteImages = speciesListViewModel.IConfigurationStore.OverwriteImages;

            List<DBObject.Species> listSpecies = speciesListViewModel.SpeciesCollection.Select(n => n as DBObject.Species).ToList();
            foreach (DBObject.Species species in listSpecies)
            {
                DBStore.DatabaseHelpers.LoadImages(speciesListViewModel.Database, speciesListViewModel.IImagePathsStore, species);
            }

            string filename = System.IO.Path.Combine(exportFolder, "Fungi.html");
            Export.PageWriterFungusList pageWriterFungusList = new Export.PageWriterFungusList(filename);
            pageWriterFungusList.WritePage(
                overwriteImages,
                exportFolder,
                "A Photographic Guide To The Fungi of Southern England",
                listSpecies,
                false);
            pageWriterFungusList.Close();

            filename = System.IO.Path.Combine(exportFolder, "CommonFungi.html");
            pageWriterFungusList.Open(filename);
            pageWriterFungusList.WritePage(
                overwriteImages,
                exportFolder,
                "Common Fungi of Southern England",
                listSpecies,
                true);
            pageWriterFungusList.Close();

            filename = System.IO.Path.Combine(exportFolder, "FungiByGroup.html");
            Export.PageWriterFungusGroups pageWriterFungusGroups = new Export.PageWriterFungusGroups(filename, listSpecies);
            pageWriterFungusGroups.WritePage("A Photographic Guide To The Fungi of Southern England", false);
            pageWriterFungusGroups.Close();

            filename = System.IO.Path.Combine(exportFolder, "CommonFungiByGroup.html");
            pageWriterFungusGroups = new Export.PageWriterFungusGroups(filename, listSpecies);
            pageWriterFungusGroups.WritePage("Common Fungi of Southern England", true);
            pageWriterFungusGroups.Close();

            Export.PageWriterPhotoIndex pageWriterPhotoIndex = new Export.PageWriterPhotoIndex(listSpecies);
            pageWriterPhotoIndex.WritePhotoIndex(exportFolder);
        }
    }
}
