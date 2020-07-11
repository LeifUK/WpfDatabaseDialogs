using System.Collections.Generic;

namespace WpfFungusApp.Export
{
    internal class PageWriterFungusGroups : PageWriterBase
    {
        public PageWriterFungusGroups(string filename, List<DBObject.Species> listSpecies) : base(filename)
        {
            _listSpecies = listSpecies;
            _onlyCommonSpecies = false;
            _fungalGenusGroups = new FungalGenusGroups();
        }

        private List<DBObject.Species> _listSpecies;
        private bool _onlyCommonSpecies;
        private FungalGenusGroups _fungalGenusGroups;

        public void WriteSpeciesInGenus(string genus)
        {
            int iGenusLength = genus.Length;

            foreach (DBObject.Species species in _listSpecies)
            {
                string speciesName = species.species;
                if ((speciesName.Length >= iGenusLength) && (speciesName.Substring(0, iGenusLength).ToUpper() == genus.ToUpper()))
                {
                    bool bWriteItem = true;
                    if (_onlyCommonSpecies)
                    {
                        // If the ocurrence field contains Common or Very common
                        string distribution = species.distribution;
                        bWriteItem = !string.IsNullOrEmpty(distribution) &&  (distribution.Contains("Common") || distribution.Contains("Very common"));
                    }

                    if (bWriteItem)
                    {
                        StreamWriter.WriteLine("  <tr>");
                        StreamWriter.WriteLine("    <td valign=top align=right width = 50%>");
                        StreamWriter.WriteLine("      <a href=\"" + speciesName + ".html\">" + speciesName + ": </ A > ");
                        StreamWriter.WriteLine("    </td>");
                        StreamWriter.WriteLine("    <td valign=top align=left width=50%>&nbsp;" + species.common_name + "</td>");
                        StreamWriter.WriteLine("  </tr>\n");
                    }
                }
            }
        }

        public void WriteGroupList(string groupName, List<string> list)
        {
            StreamWriter.WriteLine("<table cols=\"3\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 100%; background-color:white\">");
            StreamWriter.WriteLine("  <tbody>");
            StreamWriter.WriteLine("    <tr style=\"width:100%\">");
            StreamWriter.WriteLine("      <td style=\"width:15%;\"></td>");
            StreamWriter.WriteLine("      <td style=\"width:70%;\">");
            StreamWriter.WriteLine("        <A name=\"" + groupName + "\"></A><h2 class=fungi-group-header>" + groupName + "</h2>\n");
            StreamWriter.WriteLine("      </td>\n");
            StreamWriter.WriteLine("      <td style=\"width:15%;\"></td>");
            StreamWriter.WriteLine("    </tr>");
            StreamWriter.WriteLine("  </tbody>");
            StreamWriter.WriteLine("</table>\n");

            // The table of species

            StreamWriter.WriteLine("<table align = center COLS = \"2\" width = 80%  cellspacing=\"0\" cellpadding=\"2px\">");

            foreach (string item in list)
            {
                WriteSpeciesInGenus(item);
            }

            StreamWriter.WriteLine("</table>");
        }

        public void WritePage(string title, bool bOnlyCommonSpecies)
        {
            _onlyCommonSpecies = bOnlyCommonSpecies;

            StartPage("Fungi Groups");

            // Title

            StreamWriter.WriteLine("      <h1 class=fungus-index-title>");
            StreamWriter.WriteLine(title);
            StreamWriter.WriteLine("      </h1>\n");
            StreamWriter.WriteLine("      <p class=fungus-index-warning-p>");
            StreamWriter.WriteLine("        Warning: Although most fungi are harmless, a small number are extremely toxic, and should be handled with caution. ");
            StreamWriter.WriteLine("        Never eat a fungus unless you are completely certain of its identity, and the species is known to be edible. If in ");
            StreamWriter.WriteLine("        doubt, discard it.</p>");
            StreamWriter.WriteLine("      <p class=group-table-separator></p>");
            StreamWriter.WriteLine("      <table border=\"1\" bordercolorlight=darkgray bordercolordark=darkgray bgcolor=\"#F6F6F6\" align=\"center\" width=\"60%\" cellspacing=\"0\" cellpadding=\"1\" style=\"border-collapse:collapse;\">");

            StreamWriter.WriteLine("        <tbody>");

            // Create the links to the groups

            foreach (FungalGenusGroups.Group group in _fungalGenusGroups.m_list)
            {
                StreamWriter.WriteLine("    <tr style=\"vertical-align:top;\">");
                StreamWriter.WriteLine("      <td width=\"100%\" style=\"vertical-align:top;\">");
                StreamWriter.WriteLine("        <p class=group-table-item><a href=\"#" + group.Name + "\">" + group.Name + "</A></p>");
                StreamWriter.WriteLine("      </td>");
                StreamWriter.WriteLine("    </tr>");
            }

            StreamWriter.WriteLine("  </tbody>");
            StreamWriter.WriteLine("</table>");

            // Write each group
            foreach (FungalGenusGroups.Group group in _fungalGenusGroups.m_list)
            {
                WriteGroupList(group.Name, group.List);
            }

            Footer();
        }

        public void Footer()
        {
            // Add some white space

            StreamWriter.WriteLine("<p style=\"height:10px; margin-top:0px; margin-bottom:0px\"><p>");
            DrawHorizontalGreyLine();

            // Add stats

            StreamWriter.WriteLine("        <span style=\"text-align:center; font-style:italic; font-size:10pt; margin-top:0px; margin-bottom:0px\">");
            StreamWriter.WriteLine("        <p style=\"text-align:center; font-size:10pt; margin-top:3px; margin-bottom:3px\">Last updated: " +
                System.DateTime.Now.ToString() + ".</p>");
            StreamWriter.WriteLine("        </span>");
            EndPage();
        }
    }
}

