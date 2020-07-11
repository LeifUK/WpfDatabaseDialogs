using System;
using System.Collections.Generic;
namespace WpfFungusApp.DBStore
{
    interface IConfigurationStore
    {
        void CreateTable();
        void Initialise();

        string Copyright { get; set; }
        string ExportFolder { get; set; }
        bool OverwriteImages { get; set; }
    }
}
