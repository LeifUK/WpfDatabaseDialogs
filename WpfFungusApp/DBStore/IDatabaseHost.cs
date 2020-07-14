using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFungusApp.DBStore
{
    internal interface IDatabaseHost
    {
        PetaPoco.Database Database { get; set; }
        DBStore.IConfigurationStore IConfigurationStore { get; set; }
        DBStore.ISpeciesStore ISpeciesStore { get; set; }
        DBStore.IImagePathsStore IImagePathsStore { get; set; }
        DBStore.IImageStore IImageStore { get; set; }
    }
}
