using System.Collections.Generic;

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

        /// <summary>
        /// Sets the attribute key with the given attribute value
        /// </summary>
        public void setAttribute(string attributeKey, object attributeValue)
        {
            songAttributes.Add(attributeKey, attributeValue);
        }

        /// <summary>
        /// Returns the attribute value for a given key
        /// </summary>
        public object getAttribute(string attributeKey)
        {
            return songAttributes[attributeKey] != null ? songAttributes[attributeKey] : null;
        }

        /// <summary>
        /// Returns all track attributes as a Comma Separated Value row
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = string.Empty;

            foreach (string attribute in songAttributes.Keys)
                s += songAttributes[attribute] + ", ";

            return s;
        }
    }
}
