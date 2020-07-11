using System.Text;

namespace WpfFungusApp.Export
{
    class PageWriterFungus : PageWriterBase
    {
        public PageWriterFungus(string filename)
            : base(filename)
        {
        }

        /*
            Note: szFolder must have a terminating '\\' 
         */
        void ExportImage(
            bool bOverwriteImages,
            string species,
            string folder,
            DBObject.Image dBImage,
            int iIndex)
        {
            // Copy the image file into the same folder as the web file

            string filename = iIndex + "_" + System.IO.Path.GetFileName(dBImage.Path);
            string destinationPath = System.IO.Path.Combine(folder, filename);
            bool exists = System.IO.File.Exists(destinationPath);
            if (bOverwriteImages || !exists)
            {
                if (exists)
                {
                    System.IO.File.Delete(destinationPath);
                }
                System.IO.File.Copy(dBImage.Path, destinationPath);
            }

            // Create an image in a frame 

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("        <img src=\"");
            stringBuilder.Append(filename);
            stringBuilder.Append("\" border=\"0\" ALT=\"");
            stringBuilder.Append(species);
            stringBuilder.Append("\"></img><br>");
            StreamWriter.WriteLine(stringBuilder.ToString());

            stringBuilder.Clear();
            stringBuilder.Append("<p class = \"fungus-image-comment\">");
            if (!string.IsNullOrEmpty(dBImage.description))
            {
                stringBuilder.Append(dBImage.description);
                stringBuilder.Append(". ");
            }
            stringBuilder.Append("Photograph copyright ");
            stringBuilder.Append(dBImage.copyright);
            stringBuilder.Append("</p>");

            StreamWriter.WriteLine(stringBuilder.ToString());
        }

        private void WriteField(string title, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<p class=\"fungus-attribute-title\">");
                stringBuilder.Append(title);
                stringBuilder.Append("</p>");
                StreamWriter.WriteLine(stringBuilder.ToString());
                stringBuilder.Clear();

                stringBuilder.Append("<p class=\"fungus-attribute-body\">");
                stringBuilder.Append(value);
                stringBuilder.Append("</p>");
                StreamWriter.WriteLine(stringBuilder.ToString());
            }
        }

        public void Export(bool bOverwriteImages, DBObject.Species species, string path, string filename, string previousFilename, string nextFilename)
        {
            StartPage(species.species);

            // Write the title i.e. species

            StreamWriter.WriteLine("<table width=\"100%\" border=\"0\">");
            StreamWriter.WriteLine("  <tr>");
            StreamWriter.WriteLine("    <td width=\"auto\">");
            StreamWriter.WriteLine("      <h1 class=fungus-species-title>" + species.species + "</h1>");
            StreamWriter.WriteLine("    </td>");
            StreamWriter.WriteLine("    <td align=\"center\" width=\"20px\" class=\"navigation-arrows\" >");
            StreamWriter.WriteLine("      <a href=\"" + previousFilename + "\">Prev</a>");
            StreamWriter.WriteLine("    </td>");
            StreamWriter.WriteLine("    <td align=\"center\" width=\"10px\">");
            StreamWriter.WriteLine("      |");
            StreamWriter.WriteLine("    </td>");
            StreamWriter.WriteLine("    <td align=\"center\" width=\"25px\" class=\"navigation-arrows\" >");
            StreamWriter.WriteLine("      <a href=\"Fungi.html#" + species.species + "\" class=\"navigation-arrows\">Index</a>");
            StreamWriter.WriteLine("    </td>");
            StreamWriter.WriteLine("    <td align=\"center\" width=\"10px\">");
            StreamWriter.WriteLine("      |");
            StreamWriter.WriteLine("    </td>");
            StreamWriter.WriteLine("    <td align=\"center\" width=\"20px\" class=\"navigation-arrows\" >");
            StreamWriter.WriteLine("      <a href=\"" + nextFilename + "\" class=\"navigation-arrows\">Next</a>");
            StreamWriter.WriteLine("    </td>");
            StreamWriter.WriteLine("  <tr>");
            StreamWriter.WriteLine("</table>");

            // Output the first image - if any

            int iImage = 0;
            if (species.Images.Count > 0)
            {
                ExportImage(bOverwriteImages, species.species, path, species.Images[0], iImage++);
            }

            // Output the remaining fields - if any

            WriteField("Synonymns", species.synonyms);
            WriteField("Common Name", species.common_name);
            WriteField("Fruiting Body", species.fruiting_body);
            WriteField("Cap", species.cap);
            WriteField("Hymenium", species.hymenium);
            WriteField("Gills", species.gills);
            WriteField("Pores", species.pores);
            WriteField("Spines", species.spines);
            WriteField("Stem", species.stem);
            WriteField("Flesh", species.flesh);
            WriteField("Smell", species.smell);
            WriteField("Taste", species.taste);
            WriteField("Season", species.season);
            WriteField("Distribution", species.distribution);
            WriteField("Habitat", species.habitat);
            WriteField("Spore Print", species.spore_print);
            WriteField("Microscopic Features", species.microscopic_features);
            WriteField("Notes", species.notes);

            // Output the remaining images

            if (species.Images.Count > 1)
            {
                StreamWriter.WriteLine("<p class=\"fungus-image-title\">Additional Photographs</p>");
            }
            for (int i = 1; i < species.Images.Count; ++i)
            {
                ExportImage(bOverwriteImages, species.species, path, species.Images[i], i);
            }

            EndPage();
        }
    }
}
