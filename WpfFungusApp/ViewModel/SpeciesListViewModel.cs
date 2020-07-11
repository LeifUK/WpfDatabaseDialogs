using System;
using System.Collections.Generic;
using System.Linq;
using WpfFungusApp.DBStore;

namespace WpfFungusApp.ViewModel
{
    internal class SpeciesListViewModel : BaseViewModel
    {
        public SpeciesListViewModel(PetaPoco.Database database, DBStore.IConfigurationStore iConfigurationStore, DBStore.ISpeciesStore iSpeciesStore, DBStore.IImagePathsStore iImagePathsStore, DBStore.IImageStore iImageStore)
        {
            Database = database;
            IConfigurationStore = iConfigurationStore;
            ISpeciesStore = iSpeciesStore;
            IImagePathsStore = iImagePathsStore;
            IImageStore = iImageStore;

            SpeciesCollection = new System.Collections.ObjectModel.ObservableCollection<DBObject.Species>();
            Load();
        }

        public readonly DBStore.IConfigurationStore IConfigurationStore;
        public readonly DBStore.ISpeciesStore ISpeciesStore;
        public readonly DBStore.IImagePathsStore IImagePathsStore;
        public readonly DBStore.IImageStore IImageStore;

        public readonly PetaPoco.Database Database;

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
            foreach (var species in ISpeciesStore.Enumerator)
            {
                SpeciesCollection.Add(species);
            }

            return true;
        }

        private void WriteImages(DBObject.Species species)
        {
            byte displayOrder = 0;
            DatabaseHelpers.ParseImagePath(Database, species.Images);

            foreach (DBObject.Image image in species.Images)
            {
                image.display_order = displayOrder;
                ++displayOrder;
                image.fungus_id = species.id;
                if (image.id == 0)
                {
                    IImageStore.Insert(image);
                }
                else
                {
                    IImageStore.Update(image);
                }
            }
        }

        public void UpdateSpecies(DBObject.Species species, DBObject.Species editedSpecies)
        {
            Database.BeginTransaction();
            try
            {
                ISpeciesStore.Update(editedSpecies);

                List<Int64> editedImageIds = editedSpecies.Images.Select(n => n.id).ToList();
                foreach (DBObject.Image image in species.Images)
                {
                    if (!editedImageIds.Contains(image.id))
                    {
                        IImageStore.Delete(image);
                    }
                }

                WriteImages(editedSpecies);

                Database.CompleteTransaction();
            }
            catch
            {
                Database.AbortTransaction();
            }

            Load();
        }

        public void InsertSpecies(DBObject.Species species)
        {
            Database.BeginTransaction();
            try
            {
                ISpeciesStore.Insert(species);
                WriteImages(species);

                Database.CompleteTransaction();
            }
            catch
            {
                Database.AbortTransaction();
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
            DatabaseHelpers.LoadImages(Database, species);

            Database.BeginTransaction();
            try
            {
                foreach (var image in species.Images)
                {
                    IImageStore.Delete(image);
                }
                ISpeciesStore.Delete(species);

                Database.CompleteTransaction();
            }
            catch
            {
                Database.AbortTransaction();
            }
            Load();
        }
    }
}
