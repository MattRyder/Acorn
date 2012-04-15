using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acorn
{
    public class Song
    {
        public static readonly string[] Attributes = new string[] { "Track ID", "Name", "Artist", "Album Artist", 
                                        "Album", "Genre", "Kind", "Size", "Total Time", "Track Number",
                                        "Year", "Bit Rate", "Sample Rate", "Persistent ID", "Location" };

        Dictionary<string, object> songAttributes;

        public Dictionary<string, object> SongAttributes
        {
            get { return songAttributes; }
        }

        public Song() 
        {
            songAttributes = new Dictionary<string, object>();
        }

        public void setAttribute(string attributeKey, object attributeValue)
        {
            songAttributes.Add(attributeKey, attributeValue);
        }
    }
}
