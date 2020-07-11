using System.IO;

namespace WpfFungusApp.Export
{
    class PageWriterBase
    {
        public PageWriterBase(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                StreamWriter = new System.IO.StreamWriter(filename);
            }
        }

        public System.IO.StreamWriter StreamWriter { get; private set; }

        public void Open(string filename)
        {
            Close();
            StreamWriter = new System.IO.StreamWriter(filename);
        }

        public void Close()
        {
            if (StreamWriter != null)
            {
                try
                {
                    StreamWriter.Close();
                }
                catch
                {

                }
                StreamWriter = null;
            }
        }

        public bool IsOpen
        {
            get
            {
                return StreamWriter != null;
            }
        }

        public void StartPage(string title)
        {
            StreamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>");
            StreamWriter.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN");
            StreamWriter.WriteLine("\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            StreamWriter.WriteLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            StreamWriter.WriteLine("<head>");
            StreamWriter.WriteLine("  <TITLE>");
            StreamWriter.WriteLine(title);
            StreamWriter.WriteLine("</TITLE>");
            StreamWriter.WriteLine("<LINK REL='StyleSheet' HREF = \"General.css\" TYPE = \"text/css\">");
            StreamWriter.WriteLine("<SCRIPT SRC='src.js'></SCRIPT>");
            StreamWriter.WriteLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"../main.css\">");
            StreamWriter.WriteLine("</head>");
            StreamWriter.WriteLine("<body onLoad=\"init()\">");
            StreamWriter.WriteLine("<div id=\"page\">");
            StreamWriter.WriteLine("");
            StreamWriter.WriteLine("<!-- The drop down menu -->");
            StreamWriter.WriteLine("");
            StreamWriter.WriteLine("<ul id=\"nav\">");
            StreamWriter.WriteLine("  <li>");
            StreamWriter.WriteLine("    <a href=\"#\">Home</a>");
            StreamWriter.WriteLine("  </li>");
            StreamWriter.WriteLine("  <li>");
            StreamWriter.WriteLine("    <a href=\"#\">-</a>");
            StreamWriter.WriteLine("  </li>");
            StreamWriter.WriteLine("  <li>");
            StreamWriter.WriteLine("    <a href=\"#\">Fungi</a>");
            StreamWriter.WriteLine("    <ul>");
            StreamWriter.WriteLine("      <li><a href=\"../Fungi/Fungi.html\">A-Z Fungi</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../Fungi/FungiByGroup.html\">A-Z Fungi by Group</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../Fungi/CommonFungi.html\">A-Z Common Fungi</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../Fungi/CommonFungiByGroup.html\">A-Z Common Fungi by Group</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../Fungi/FungusPhotoGroupIndex.html\">Fungi Photo Index</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../General/A-Z Notes.html\">A-Z Notes</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../General/Colours.html\">Colour Chart</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../General/Fungi Basic Key.html\">Basic Keys</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../General/Microscopy.html\">Microscopy</a></li>");
            StreamWriter.WriteLine("    </ul>");
            StreamWriter.WriteLine("  </li>");
            StreamWriter.WriteLine("  <li>");
            StreamWriter.WriteLine("    <a href=\"#\">-</a>");
            StreamWriter.WriteLine("  </li>");
            StreamWriter.WriteLine("  <li>");
            StreamWriter.WriteLine("    <a href=\"#\">Photography</a>");
            StreamWriter.WriteLine("    <ul>");
            StreamWriter.WriteLine("      <li><a href=\"../Favourites/Favourites.html\">Favourites</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../Butterflies/Butterflies.html\">Butterflies</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../Dragonflies/Dragonflies.html\">Dragonflies</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../Damselflies/Damselflies.html\">Damselflies</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../Other Insects/Other Insects.html\">Other Insects</a></li>");
            StreamWriter.WriteLine("      <li><a <a href=\"../Flora/Flora.html\">Flora</a></li>");
            StreamWriter.WriteLine("      <li><a <a href=\"../General/Equipment.html\">Equipment</a></li>");
            StreamWriter.WriteLine("    </ul>");
            StreamWriter.WriteLine("  </li>");
            StreamWriter.WriteLine("  <li>");
            StreamWriter.WriteLine("    <a href=\"#\">-</a>");
            StreamWriter.WriteLine("  </li>");
            StreamWriter.WriteLine("  <li>");
            StreamWriter.WriteLine("    <a href=\"#\">Essays</a>");
            StreamWriter.WriteLine("    <ul>");
            StreamWriter.WriteLine("      <li><a href=\"../DigitalVersusFilm/Velvia100-versus-D200.html\">Digital versus Film</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../ES1 Review/ES1-Review.html\">Nikon ES1 Review</a></li>");
            StreamWriter.WriteLine("    </ul>");
            StreamWriter.WriteLine("  </li>");
            StreamWriter.WriteLine("  <li>");
            StreamWriter.WriteLine("    <a href=\"#\">-</a>");
            StreamWriter.WriteLine("  </li>");
            StreamWriter.WriteLine("  <li>");
            StreamWriter.WriteLine("    <a href=\"#\">Misc</a>");
            StreamWriter.WriteLine("    <ul>");
            StreamWriter.WriteLine("      <li><a href=\"../General/Bibliography.html\">Bibliography</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../General/Booksellers.html\">Booksellers</a></li>");
            StreamWriter.WriteLine("      <li><a href=\"../General/Links.html\">Links</a></li>");
            StreamWriter.WriteLine("            <li><a href=\"../General/About.html\">About</a></li>");
            StreamWriter.WriteLine("            <li><a href=\"../General/Feedback.html\">Feedback</a></li>");
            StreamWriter.WriteLine("        </ul>");
            StreamWriter.WriteLine("    </li>");
            StreamWriter.WriteLine("</ul>");
            StreamWriter.WriteLine("");
            StreamWriter.WriteLine("<!-- The page frame -->");
            StreamWriter.WriteLine("");
            StreamWriter.WriteLine("<table cellspacing=\"0\" cellpadding=\"0\"");
            StreamWriter.WriteLine("style=\"width: 940px; height:1px; background-color:white\">");
            StreamWriter.WriteLine("  <tbody>");
            StreamWriter.WriteLine("    <tr>");
            StreamWriter.WriteLine("      <td colspan=\"3\" height=\"10px\" bgcolor=\"white\"></td>");
            StreamWriter.WriteLine("    </tr>");
            StreamWriter.WriteLine("  </tbody>");
            StreamWriter.WriteLine("</table>");
            StreamWriter.WriteLine("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"");
            StreamWriter.WriteLine("style=\"width: 940px; border:0px ; border-color:darkgray; background-color:white; margin:0px\">");
            StreamWriter.WriteLine("  <tbody>");
            StreamWriter.WriteLine("    <tr>");
            StreamWriter.WriteLine("      <td style=\"width:30px; background-color:white; border-style:none\"></td>");
            StreamWriter.WriteLine("      <td style=\"width:auto; height: 100%; background-color:white;\">");
            StreamWriter.WriteLine("");
        }

        public void EndPage()
        {
            StreamWriter.WriteLine("");
            StreamWriter.WriteLine(" <!-- The page frame -->");
            StreamWriter.WriteLine("");
            StreamWriter.WriteLine("      </td>");
            StreamWriter.WriteLine("      <td style=\"width:30px; background-color:white\"></td>");
            StreamWriter.WriteLine("    </tr>");
            StreamWriter.WriteLine("  </tbody>");
            StreamWriter.WriteLine("</table>");
            StreamWriter.WriteLine("<table cellspacing=\"0\" cellpadding=\"0\" style=\"width: 940px; height:5px; background-color:white\">");
            StreamWriter.WriteLine("  <tbody>");
            StreamWriter.WriteLine("    <tr>");
            StreamWriter.WriteLine("      <td colspan=\"3\" height=\"20px\"></td>");
            StreamWriter.WriteLine("    </tr>");
            StreamWriter.WriteLine("  </tbody>");
            StreamWriter.WriteLine("</table>");

            // That's all folks!

            StreamWriter.WriteLine("</div>");
            StreamWriter.WriteLine("</body>");
            StreamWriter.WriteLine("</html>");
        }

        public void DrawHorizontalGreyLine()
        {
            // Draw a horizontal grey line

            StreamWriter.WriteLine("<table align=\"center\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 40%; background-color:white\">");

            StreamWriter.WriteLine("  <tbody>");


              StreamWriter.WriteLine("    <tr><td height=\"4px\"></td></tr>");


              StreamWriter.WriteLine("    <tr style=\"width:auto\">");


              StreamWriter.WriteLine("      <td style=\"height:1px; height:1px; background-color:darkgray;\"></td>");


              StreamWriter.WriteLine("    </tr>");


              StreamWriter.WriteLine("    <tr style=\"width:auto\">");


              StreamWriter.WriteLine("      <td style=\"height:10px;\"></td>");


              StreamWriter.WriteLine("    </tr>");


              StreamWriter.WriteLine("  </tbody>");


              StreamWriter.WriteLine("</table>");
        }

    }
}
