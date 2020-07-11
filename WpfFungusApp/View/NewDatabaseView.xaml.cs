using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfFungusApp.View
{
    /// <summary>
    /// Interaction logic for NewDatabaseView.xaml
    /// </summary>
    public partial class NewDatabaseView : Window
    {
        public NewDatabaseView()
        {
            InitializeComponent();
        }

        private void _buttonBrowse_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog == null)
            {
                return;
            }

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ViewModel.NewDatabaseViewModel newDatabaseViewModel = DataContext as ViewModel.NewDatabaseViewModel;
                newDatabaseViewModel.Folder = dialog.SelectedPath;
            }
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
