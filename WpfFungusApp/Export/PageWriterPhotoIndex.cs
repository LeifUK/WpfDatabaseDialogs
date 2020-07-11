using System;
using System.Collections.Generic;
using System.Text;

namespace WpfFungusApp.Export
{
    class PageWriterPhotoIndex
    {
        public PageWriterPhotoIndex(List<DBObject.Species> listSpecies)
        {
            _listSpecies = listSpecies;
            _fungalGenusGroups = new FungalGenusGroups();
        }

        private readonly List<DBObject.Species> _listSpecies;
        private readonly FungalGenusGroups _fungalGenusGroups;

        void WriteGroupPage(string path, FungalGenusGroups.Group group)
        {
            string filename = path;
            filename += group.Name;
            filename += ".html";

            PageWriterBase page = new PageWriterBase(filename);
            page.StartPage(group.Name);

            page.StreamWriter.WriteLine("<h1 class=fungus-group-title>" + group.Name + "</h1>");

            foreach (DBObject.Species species in _listSpecies)
            {
                string speciesName = species.species;
                bool bFound = false;
                foreach (string genus in group.List)
                {
                    if (speciesName.Length > genus.Length)
                    {
                        bFound = genus.Equals(speciesName.Substring(0, genus.Length), StringComparison.OrdinalIgnoreCase);
                        if (bFound)
                        {
                            break;
                        }
                    }
                }

                if (bFound)
                {
                    page.StreamWriter.WriteLine("  <br/><p align=\"left\"><a href=\"" + speciesName + ".html\">" + speciesName + "</A></p>");

                    // Output the first image - if any
                    if (species.Images.Count > 0)
                    {
                        // Create an image in a frame 

                        DBObject.Image dbImage = species.Images[0];
                        string imageFile = System.IO.Path.GetFileName(dbImage.Path);
                        page.StreamWriter.WriteLine("        <img src=\"0_" + imageFile + "\" border=\"0\" ALT=\"" + imageFile + "\"></img><br>");

                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.Append("<p class = \"fungus-image-comment\">");
                        if (!string.IsNullOrEmpty(dbImage.description))
                        {
                            stringBuilder.Append(dbImage.description);
                            stringBuilder.Append(". ");
                        }
                        stringBuilder.Append("Photograph copyright " + dbImage.copyright + "</p>");
                        page.StreamWriter.WriteLine(stringBuilder.ToString());
                    }
                }
            }

            page.StreamWriter.WriteLine("        <br/>");
            page.StreamWriter.WriteLine("        <br/>");
            page.DrawHorizontalGreyLine();

            // Add stats

            page.StreamWriter.WriteLine("        <span style=\"text-align:center; font-style:italic; font-size:10pt; margin-top:0px; margin-bottom:0px\">");
            page.StreamWriter.WriteLine("        <p style=\"text-align:center; font-size:10pt; margin-top:3px; margin-bottom:3px\">Last updated: " + System.DateTime.Now.ToString() + ".</p>");
            page.StreamWriter.WriteLine("        </span>\n");

            page.EndPage();
        }

        public void WritePhotoIndex(string path)
        {
            /*
                Create a photo index file => path + "FungusPhotoGroupIndex.html"
            */
            string filename = System.IO.Path.Combine(path, "FungusPhotoGroupIndex.html");

            PageWriterBase page = new PageWriterBase(filename);

            page.StartPage("Fungi Photo Index");

            page.StreamWriter.WriteLine("    <h1 class=fungus-photo-index-title>Fungi Photo Index</h1>");

            page.DrawHorizontalGreyLine();
            page.StreamWriter.WriteLine("    <p class=fungus-index-warning-p>");

            page.StreamWriter.WriteLine("      Warning: Although most fungi are harmless, a small number are extremely toxic, and should be handled with caution. ");
            page.StreamWriter.WriteLine("      Never eat a fungus unless you are completely certain of its identity, and the species is known to be edible. If in ");
            page.StreamWriter.WriteLine("      doubt, discard it.</p>");

            page.DrawHorizontalGreyLine();

            page.StreamWriter.WriteLine("    <p class=group-table-separator></p>");
            page.StreamWriter.WriteLine("    <table align=\"center\" width=\"60%\" cellspacing=\"0\" cellpadding=\"1\" style=\"border-collapse:collapse;\">");
            page.StreamWriter.WriteLine("      <tbody>");

            foreach (FungalGenusGroups.Group group in _fungalGenusGroups.m_list)
            {
                page.StreamWriter.WriteLine("    <tr style=\"vertical-align:top;\">");
                page.StreamWriter.WriteLine("      <td width=\"100%\" style=\"vertical-align:top;\">");
                page.StreamWriter.WriteLine("        <p class=group-table-item><a href=\"" + group.Name + ".html\">" + group.Name + "</A></p>");
                page.StreamWriter.WriteLine("      </td>");
                page.StreamWriter.WriteLine("    </tr>");

                WriteGroupPage(path, group);
            }

            page.StreamWriter.WriteLine("  </tbody>");
            page.StreamWriter.WriteLine("</table>");

            // Add some white space

            page.StreamWriter.WriteLine("<p style=\"height:10px; margin-top:0px; margin-bottom:0px\"><p>");

            page.DrawHorizontalGreyLine();

            // Add stats

            page.StreamWriter.WriteLine("        <span style=\"text-align:center; font-style:italic; font-size:10pt; margin-top:0px; margin-bottom:0px\">");
            page.StreamWriter.WriteLine("        <p style=\"text-align:center; font-size:10pt; margin-top:3px; margin-bottom:3px\">Last updated: " + System.DateTime.Now.ToString() + ".</p>");
            page.StreamWriter.WriteLine("        </span>");

            page.EndPage();
            page.Close();
        }
    }
}
