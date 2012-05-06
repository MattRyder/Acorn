using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Acorn
{
    /// <summary>
    /// Loads and parses through the iTunes Music Library.xml file.
    /// </summary>
    class LibraryParser
    {
        string libraryLocation;
        List<Song> songs;

        XmlReader reader;
        XmlReaderSettings settings;

        /// <summary>
        /// LibraryParser Constructor
        /// </summary>
        /// <param name="libraryLocation">Location of the iTunes Music Library.xml file</param>
        public LibraryParser(string libraryLocation)
        {
            this.libraryLocation = libraryLocation;

            settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;

            reader = XmlReader.Create(libraryLocation, settings);
        }

        /// <summary>
        /// Parses the iTunes Music Library.xml for all audio files
        /// </summary>
        /// <returns>A List of Song objects</returns>
        public List<Song> parse()
        {
            int dictCount = 0;
            bool isValidSong = false;
            string attributeKey;

            List<Song> songs = new List<Song>();
            Song song = new Song();

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "dict"))
                {
                    dictCount++; //Current node is a <dict> element, increment count
                    song = new Song(); //Create a new Song object
                }

                else if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "dict"))
                {
                    dictCount--; //Current node is a </dict> element, decrement count

                    //Did we just leave a Song's dictionary? Is it a valid song?
                    if ((song.SongAttributes.Count() > 0) && isValidSong)
                    {
                        songs.Add(song);
                    }
                    isValidSong = true; //Reset valid song flag
                }

                if ((dictCount == 3) && ((attributeKey = getKeyNodeValue()) != null))
                {
                    if (Song.Attributes.Contains(attributeKey))
                    {
                        object attributeValue = getAttributeNodeValue();
                        if ((attributeKey == "Kind") && (attributeValue.ToString().IndexOf("audio") < 0))
                        {
                            isValidSong = false; //Not an audio file
                        }

                        song.setAttribute(attributeKey, attributeValue);
                    }
                }

                //Finished collecting songs, and left the "Tracks" dict?
                if ((songs.Count() > 0) && (dictCount < 2))
                    return songs;
            }
            return songs;
        }

        /// <summary>
        /// Gets the current node value of reader if the current node is a <key> element
        /// </summary>
        /// <returns></returns>
        private string getKeyNodeValue()
        {
            XElement element;
            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "key"))
            {
                element = (XElement)XNode.ReadFrom(reader);
                return element.Value;
            }
            return null;
        }

        /// <summary>
        /// Returns a parsed object from it's attribute tags
        /// </summary>
        /// <returns></returns>
        private object getAttributeNodeValue()
        {
            XElement element = (XElement)XNode.ReadFrom(reader);
            string elementName = element.Name.LocalName;

            try
            {
                if (elementName == "integer")
                    return Convert.ToInt32(element.Value);

                if (elementName == "string")
                    return element.Value.ToString();

                if (elementName == "true")
                    return true;

                if (elementName == "false")
                    return false;

                if (elementName == "date")
                    return DateTime.Parse(element.Value);
            }
            catch (Exception e)
            {
                throw e;
            }

            //Unhandled data type, return string value:
            return elementName.ToString();
        }
    }
}
