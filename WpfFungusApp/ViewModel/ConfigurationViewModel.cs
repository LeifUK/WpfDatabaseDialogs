using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using WpfFungusApp.DBObject;

namespace WpfFungusApp.ViewModel
{
    class ConfigurationViewModel : BaseViewModel
    {
        public ConfigurationViewModel(DBStore.IConfigurationStore iConfigurationStore, DBStore.IImagePathsStore iImagePathsStore, DBStore.IImageStore iImageStore)
        {
            _iConfigurationStore = iConfigurationStore;
            _iConfigurationStore.Initialise();
            _iImagePathsStore = iImagePathsStore;
            _iImageStore = iImageStore;

            ImagePaths = new System.Collections.ObjectModel.ObservableCollection<ImagePath>(_iImagePathsStore.Enumerator);
        }

        private readonly DBStore.IConfigurationStore _iConfigurationStore;
        private readonly DBStore.IImagePathsStore _iImagePathsStore;
        private readonly DBStore.IImageStore _iImageStore;

        public string Copyright
        {
            get
            {
                return _iConfigurationStore.Copyright;
            }
            set
            {
                _iConfigurationStore.Copyright = value;
                NotifyPropertyChanged("Copyright");
            }
        }

        public string ExportFolder
        {
            get
            {
                return _iConfigurationStore.ExportFolder;
            }
            set
            {
                _iConfigurationStore.ExportFolder = value;
                NotifyPropertyChanged("ExportFolder");
            }
        }

        public bool OverwriteImages
        {
            get
            {
                return _iConfigurationStore.OverwriteImages;
            }
            set
            {
                _iConfigurationStore.OverwriteImages = value;
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
            _iImagePathsStore.Update(imagePath);
        }

        public void AddImagePath(string path)
        {
            DBObject.ImagePath imagePath = new ImagePath();
            imagePath.path = path;
            _iImagePathsStore.Insert(imagePath);
            ImagePaths.Add(imagePath);
        }

        public void DeleteImagePath(ImagePath imagePath)
        {
            if (_iImageStore.Exists(imagePath))
            {
                System.Windows.Forms.MessageBox.Show("Unable to remove the folder: it is referenced by one or more images");
                return;
            }

            _iImagePathsStore.Delete(imagePath);
            ImagePaths.Remove(imagePath);
        }
    }
}
