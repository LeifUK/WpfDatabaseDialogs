using System.Collections.Generic;
using System.Data.Common;
using System.IO;

namespace WpfFungusApp.Export
{
    class PageWriterFungusList : PageWriterBase
    {
        public PageWriterFungusList(string filename) : base(filename)
        {

        }

        private uint m_iNumberOfSpecies;
        private uint m_iNumberOfImages;
        private string m_previousSpecies;
        private bool m_bOnlyCommonSpecies;

        public void WriteHeader(string title)
        {
            if (!IsOpen)
            {
                return;
            }

            StartPage("The Fungi of Southern England");

            // Title
            StreamWriter.Write("        <h1 class=fungus-index-title>");
            StreamWriter.Write(title);
            StreamWriter.WriteLine("        </h1>");
            StreamWriter.WriteLine("        <p class=fungus-index-warning-p>");
            StreamWriter.WriteLine("          Warning: Although most fungi are harmless, a small number are extremely toxic, and should be handled with caution. ");
            StreamWriter.WriteLine("          Never eat a fungus unless you are completely certain of its identity, and the species is known to be edible. If in ");
            StreamWriter.WriteLine("          doubt, discard it.</p>");

            // Draw a horizontal grey line

            StreamWriter.WriteLine("        <p class=species-separator-one></p>");

            // Alphabetical links 

            StreamWriter.WriteLine("        <table border=1px bordercolorlight=gray bordercolordark=gray align = center COLS = \"26\" cellspacing=\"0\" cellpadding=\"4px\" style=\"border-collapse:collapse;\">");
            StreamWriter.WriteLine("          <tbody>");
            StreamWriter.WriteLine("            <tr>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#A\">A</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#B\">B</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#C\">C</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#D\">D</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#E\">E</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#F\">F</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#G\">G</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#H\">H</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#I\">I</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#J\">J</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#K\">K</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#L\">L</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#M\">M</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#N\">N</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#O\">O</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#P\">P</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#Q\">Q</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#R\">R</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#S\">S</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#T\">T</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#U\">U</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#V\">V</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#W\">W</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#X\">X</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#Y\">Y</A></td>");
            StreamWriter.WriteLine("              <td class=alphabet-table-item><a href=\"#Z\">Z</A></td>");
            StreamWriter.WriteLine("            </tr>");
            StreamWriter.WriteLine("          </tbody>");
            StreamWriter.WriteLine("        </table>");

            // Draw a horizontal grey line

            StreamWriter.WriteLine("<p class=species-separator-two></p>");

            // The main table of species

            StreamWriter.WriteLine("        <table align = center COLS = \"2\" width = 80%%  cellspacing=\"0\" cellpadding=\"2px\">");
        }

        private void AddItem(string species, string commonName, string filename)
        {
            m_iNumberOfSpecies++;

            StreamWriter.WriteLine("          <tr>");
            StreamWriter.WriteLine("            <td valign=top align=right width = 50%%>");

            // Add a link to the first species with a given first letter
            if (string.IsNullOrEmpty(m_previousSpecies) || (m_previousSpecies[0] != species[0]))
            {
                StreamWriter.WriteLine(string.Format("              <div id=\"{0}\"></div>", species[0]));
            }

            m_previousSpecies = species;

            StreamWriter.WriteLine("              <a name=\"" + species + "\"></a><a href=\"" + filename + "\">" + species + ": </A>");
            StreamWriter.WriteLine("            </td>");
            StreamWriter.WriteLine("            <td valign=top align=left width=50%%>&nbsp;" + commonName + "</td>");
            StreamWriter.WriteLine("          </tr>");
        }

        private void WriteFooter()
        {
            StreamWriter.WriteLine("        </table>");

            // Add some white space

            StreamWriter.WriteLine("<p style=\"height:10px; margin-top:0px; margin-bottom:0px\"><p>");

            // Draw a horizontal grey line
            DrawHorizontalGreyLine();

            // Add stats

            StreamWriter.WriteLine("        <span style=\"text-align:center; font-style:italic; font-size:10pt; margin-top:0px; margin-bottom:0px\">");
            StreamWriter.WriteLine("        <p class=fungus-index-species-count> " + m_iNumberOfSpecies + " species, " + m_iNumberOfImages + " images.</p>");
            StreamWriter.WriteLine("        <p class=fungus-index-date>Last updated: " + System.DateTime.Now.ToString() + ".</p>");
            StreamWriter.WriteLine("        </span>");

            EndPage();
        }

        public void WritePage(
            bool bOverwrite,
            string path,
            string title,
            List<DBObject.Species> listSpecies,
            bool bOnlyCommonSpecies)
        {
            m_bOnlyCommonSpecies = bOnlyCommonSpecies;

            WriteHeader(title);

            m_iNumberOfImages = 0;
            string previousFilename = "";
            string filename;
            string nextFilename;
            for (int index = 0; index < listSpecies.Count; ++index)
            {
                DBObject.Species species = listSpecies[index];
                bool bWriteItem = true;
                if (m_bOnlyCommonSpecies)
                {
                    // If the ocurrence field contains Common or Very common
                    string distribution = species.distribution;
                    bWriteItem = !string.IsNullOrEmpty(distribution) && (distribution.Contains("Common") || distribution.Contains("Very common"));
                }

                if (bWriteItem)
                {
                    string fileAndPath = System.IO.Path.Combine(path, species.species + ".html");

                    filename = species.species + ".html";

                    m_iNumberOfImages += (uint)species.Images.Count;
                    string commonName = species.common_name;

                    if (index < (listSpecies.Count - 2))
                    {
                        nextFilename = listSpecies[index + 1].species + ".html";
                    }
                    else
                    {
                        nextFilename = null;
                    }

                    if (!m_bOnlyCommonSpecies)
                    {
                        PageWriterFungus pageWriterFungus = new PageWriterFungus(fileAndPath);
                        pageWriterFungus.Export(
                            bOverwrite,
                            species,
                            path,
                            filename,
                            previousFilename,
                            nextFilename);
                        pageWriterFungus.Close();
                    }

                    AddItem(species.species, commonName, filename);

                    previousFilename = filename;
                }
            }

            WriteFooter();
        }
    }
}
