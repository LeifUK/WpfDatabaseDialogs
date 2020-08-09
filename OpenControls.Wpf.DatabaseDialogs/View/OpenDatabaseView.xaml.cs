using System.Windows;

namespace OpenControls.Wpf.DatabaseDialogs.View
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenDatabaseViewModel openDatabaseViewModel = DataContext as ViewModel.OpenDatabaseViewModel;
            if (openDatabaseViewModel.IDatabaseConfiguration.SavePassword)
            {
                _passwordBoxSQLServer.Password = openDatabaseViewModel.SQLServer_Password;
                _passwordBoxPostgreSQL.Password = openDatabaseViewModel.PostgreSQL_Password;
                _passwordBoxMySQL.Password = openDatabaseViewModel.MySQL_Password;
            }
        }

        private void _buttonBrowseSQLiteDatabases_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog == null)
            {
                return;
            }

            ViewModel.OpenDatabaseViewModel openDatabaseViewModel = DataContext as ViewModel.OpenDatabaseViewModel;
            openFileDialog.FileName = openDatabaseViewModel.SQLite_Filename;
            openFileDialog.Filter = "SQLite Database (*.sqlite)|*.sqlite|Microsoft SQL Server Database(*.mdf) | *.mdf";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                openDatabaseViewModel.SQLite_Filename = openFileDialog.FileName;
            }
        }

        private void _buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenDatabaseViewModel openDatabaseViewModel = DataContext as ViewModel.OpenDatabaseViewModel;
            openDatabaseViewModel.Refresh();
        }

        private void _buttonOK_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenDatabaseViewModel openDatabaseViewModel = DataContext as ViewModel.OpenDatabaseViewModel;
            openDatabaseViewModel.SQLServer_Password = _passwordBoxSQLServer.Password;
            openDatabaseViewModel.PostgreSQL_Password = _passwordBoxPostgreSQL.Password;
            openDatabaseViewModel.MySQL_Password = _passwordBoxMySQL.Password;
            DialogResult = true;
        }

        private void _buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
