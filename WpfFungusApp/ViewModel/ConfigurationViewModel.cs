using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using WpfFungusApp.DBObject;

namespace WpfFungusApp.ViewModel
{
    class ConfigurationViewModel : BaseViewModel
    {
        public ConfigurationViewModel(DBStore.IDatabaseHost iDatabaseHost)
        {
            IDatabaseHost = iDatabaseHost;
            IDatabaseHost.IConfigurationStore.Initialise();

            ImagePaths = new System.Collections.ObjectModel.ObservableCollection<ImagePath>(IDatabaseHost.IImagePathsStore.Enumerator);
        }

        private readonly DBStore.IDatabaseHost IDatabaseHost;

        public string Copyright
        {
            get
            {
                return IDatabaseHost.IConfigurationStore.Copyright;
            }
            set
            {
                IDatabaseHost.IConfigurationStore.Copyright = value;
                NotifyPropertyChanged("Copyright");
            }
        }

        public string ExportFolder
        {
            get
            {
                return IDatabaseHost.IConfigurationStore.ExportFolder;
            }
            set
            {
                IDatabaseHost.IConfigurationStore.ExportFolder = value;
                NotifyPropertyChanged("ExportFolder");
            }
        }

        public bool OverwriteImages
        {
            get
            {
                return IDatabaseHost.IConfigurationStore.OverwriteImages;
            }
            set
            {
                IDatabaseHost.IConfigurationStore.OverwriteImages = value;
                NotifyPropertyChanged("OverwriteImages");
            }
        }

        private System.Collections.ObjectModel.ObservableCollection<DBObject.ImagePath> _imagePaths;
        public System.Collections.ObjectModel.ObservableCollection<DBObject.ImagePath> ImagePaths
        {
            get
            {
                return _imagePaths;
            }
            set
            {
                _imagePaths = value;
                NotifyPropertyChanged("ImagePaths");
            }
        }

        public void UpdateImagePath(ImagePath imagePath)
        {
            IDatabaseHost.IImagePathsStore.Update(imagePath);
        }

        public void AddImagePath(string path)
        {
            DBObject.ImagePath imagePath = new ImagePath();
            imagePath.path = path;
            IDatabaseHost.IImagePathsStore.Insert(imagePath);
            ImagePaths.Add(imagePath);
        }

        public void DeleteImagePath(ImagePath imagePath)
        {
            if (IDatabaseHost.IImageStore.Exists(imagePath))
            {
                System.Windows.Forms.MessageBox.Show("Unable to remove the folder: it is referenced by one or more images");
                return;
            }

            IDatabaseHost.IImagePathsStore.Delete(imagePath);
            ImagePaths.Remove(imagePath);
        }
    }
}
