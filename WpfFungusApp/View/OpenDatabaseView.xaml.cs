using System.Windows;

namespace WpfFungusApp.View
{
    /// <summary>
    /// Interaction logic for OpenDatabaseView.xaml
    /// </summary>
    public partial class OpenDatabaseView : Window
    {
        public OpenDatabaseView()
        {
            InitializeComponent();
        }

        private void _buttonBrowseSQLiteDatabases_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenDatabaseViewModel openDatabaseViewModel = DataContext as ViewModel.OpenDatabaseViewModel;
            openDatabaseViewModel.Refresh();
        }

        private void _buttonOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void _buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
