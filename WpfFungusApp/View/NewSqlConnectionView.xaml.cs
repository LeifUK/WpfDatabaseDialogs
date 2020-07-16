using System.Windows;

namespace WpfFungusApp.View
{
    /// <summary>
    /// Interaction logic for SqlConnectionDialog.xaml
    /// </summary>
    public partial class NewSqlConnectionView : Window
    {
        public NewSqlConnectionView()
        {
            InitializeComponent();
        }

        private void _buttonBrowseFolders_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog == null)
            {
                return;
            }

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ViewModel.NewSqlConnectionViewModel sqlConnectionViewModel = DataContext as ViewModel.NewSqlConnectionViewModel;
                sqlConnectionViewModel.Folder = dialog.SelectedPath;
            }
        }
    }
}
