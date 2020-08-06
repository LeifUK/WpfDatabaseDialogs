using System;
using System.Collections.Generic;
using System.Linq;
using WpfFungusApp.DBStore;

namespace WpfFungusApp.ViewModel
{
    internal class SpeciesListViewModel : OpenControls.Wpf.DatabaseDialogs.ViewModel.BaseViewModel
    {
        public SpeciesListViewModel(IDatabaseHost iDatabaseHost)
        {
            IDatabaseHost = iDatabaseHost;

            SpeciesCollection = new System.Collections.ObjectModel.ObservableCollection<DBObject.Species>();
            Load();
        }

        public readonly IDatabaseHost IDatabaseHost;

        private System.Collections.ObjectModel.ObservableCollection<DBObject.Species> _speciesCollection;
        public System.Collections.ObjectModel.ObservableCollection<DBObject.Species> SpeciesCollection
        {
            get
            {
                return _speciesCollection;
            }
            set
            {
                _speciesCollection = value;
                NotifyPropertyChanged("SpeciesCollection");
            }
        }

        private DBObject.Species _selectedSpecies;
        public DBObject.Species SelectedSpecies
        {
            get
            {
                return _selectedSpecies;
            }
            set
            {
                _selectedSpecies = value;
                NotifyPropertyChanged("SelectedSpecies");
            }
        }

        public bool Load()
        {
            SpeciesCollection.Clear();
            foreach (var species in IDatabaseHost.ISpeciesStore.Enumerator)
            {
                SpeciesCollection.Add(species);
            }

            return true;
        }

        internal void LoadImages(DBObject.Species species)
        {
            Dictionary<long, string> paths = IDatabaseHost.IImagePathsStore.LoadImagePaths();

            species.Images = IDatabaseHost.IImageStore.LoadImages(species.id, paths);
        }

        private static bool ParseImagePath(IImagePathsStore iImagePathsStore, List<DBObject.Image> images)
        {
            Dictionary<long, string> paths = iImagePathsStore.LoadImagePaths();

            foreach (var image in images)
            {
                foreach (KeyValuePair<long, string> keyValuePair in paths)
                {
                    if (image.Path.Contains(keyValuePair.Value))
                    {
                        image.image_database_id = keyValuePair.Key;
                        image.filename = image.Path.Substring(keyValuePair.Value.Length);
                        break;
                    }
                }
            }

            return false;
        }

        private void WriteImages(DBObject.Species species)
        {
            byte displayOrder = 0;
            ParseImagePath(IDatabaseHost.IImagePathsStore, species.Images);

            foreach (DBObject.Image image in species.Images)
            {
                image.display_order = displayOrder;
                ++displayOrder;
                image.fungus_id = species.id;
                if (image.id == 0)
                {
                    IDatabaseHost.IImageStore.Insert(image);
                }
                else
                {
                    IDatabaseHost.IImageStore.Update(image);
                }
            }
        }

        public void UpdateSpecies(DBObject.Species species, DBObject.Species editedSpecies)
        {
            IDatabaseHost.Database.BeginTransaction();
            try
            {
                IDatabaseHost.ISpeciesStore.Update(editedSpecies);

                List<Int64> editedImageIds = editedSpecies.Images.Select(n => n.id).ToList();
                foreach (DBObject.Image image in species.Images)
                {
                    if (!editedImageIds.Contains(image.id))
                    {
                        IDatabaseHost.IImageStore.Delete(image);
                    }
                }

                WriteImages(editedSpecies);

                IDatabaseHost.Database.CompleteTransaction();
            }
            catch
            {
                IDatabaseHost.Database.AbortTransaction();
            }

            Load();
        }

        public void InsertSpecies(DBObject.Species species)
        {
            IDatabaseHost.Database.BeginTransaction();
            try
            {
                IDatabaseHost.ISpeciesStore.Insert(species);
                WriteImages(species);

                IDatabaseHost.Database.CompleteTransaction();
            }
            catch (Exception exception)
            {
                IDatabaseHost.Database.AbortTransaction();
            }
            Load();
            var enumerator = SpeciesCollection.Where(n => n.id == species.id);
            if ((enumerator != null) && (enumerator.Count() > 0))
            {
                SelectedSpecies = enumerator.First();
            }
        }

        public void DeleteSpecies(int index)
        {
            DBObject.Species species = SelectedSpecies;
            LoadImages(species);

            IDatabaseHost.Database.BeginTransaction();
            try
            {
                foreach (var image in species.Images)
                {
                    IDatabaseHost.IImageStore.Delete(image);
                }
                IDatabaseHost.ISpeciesStore.Delete(species);

                IDatabaseHost.Database.CompleteTransaction();
            }
            catch
            {
                IDatabaseHost.Database.AbortTransaction();
            }
            Load();
        }
    }
}
