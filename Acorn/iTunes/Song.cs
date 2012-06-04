using System.Collections.Generic;

namespace Acorn.iTunes
{
    public class Song
    {
        public static readonly string[] Attributes = new string[] { "Track ID", "Name", "Artist", "Album Artist", 
                                                  "Album", "Genre", "Kind", "Size", "Total Time", "Track Number",
                                                  "Track Count", "Year", "Date Modified", "Date Added", "Bit Rate",
                                                  "Sample Rate", "Comments", "Skip Count", "Skip Date", "Persistent ID", 
                                                  "Location" };

        Dictionary<string, object> songAttributes;

        public Dictionary<string, object> SongAttributes
        {
            get { return songAttributes; }
        }

        /// <summary>
        /// An object representation of a Song
        /// </summary>
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
            if(songAttributes.ContainsKey(attributeKey))
                return songAttributes[attributeKey];
            else return null;
        }
    }
}
