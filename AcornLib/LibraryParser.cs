using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public List<Song> getSongsFromLibrary()
        {
            Song song;
            songs = new List<Song>();
            bool validSong;
            int attribCount;

            while (getCurrentKeyNodeValue() != "Tracks")
                reader.Read();

            while (reader.Depth < 3)
                moveToNextDict();

            while (!reader.EOF)
            {
                song = new Song();
                validSong = true;
                attribCount = 0;

                while (!isEndOfDict())
                {
                    reader.Read();

                    string attributeKey = getCurrentKeyNodeValue();

                    if (Song.Attributes.Contains(attributeKey))
                    {
                        object attributeValue = getAttributeNodeValue();

                        if ((attributeKey == "Kind") && (attributeValue.ToString().IndexOf("audio") < 0))
                        {
                            validSong = false;
                            break; //Not an audio file
                        }

                        song.setAttribute(attributeKey, attributeValue);
                        attribCount++;
                    }
                }

                if(validSong && attribCount > 4)
                    songs.Add(song);

                moveToNextDict();
            }
            return songs;
        }

        /// <summary>
        /// Gets the current node value of reader if the current node is a <key> element
        /// </summary>
        /// <returns></returns>
        private string getCurrentKeyNodeValue()
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

            return parseTypeFromXElement(element);
        }

        /// <summary>
        /// Moves to the next <dict> node in the stream
        /// </summary>
        private void moveToNextDict()
        {
            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "dict"))
                    return;
            }
        }

        /// <summary>
        /// Checks whether the current node is a </dict> tag
        /// </summary>
        private bool isEndOfDict()
        {
            if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "dict"))
                return true;
            else return false;
        }

        /// <summary>
        /// Pulls the Data Type from the XML Tag. E.g. int type from "<integer>"
        /// </summary>
        /// <param name="element">Element to parse</param>
        /// <returns>Object of it's given type</returns>
        private object parseTypeFromXElement(XElement element)
        {
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
                Console.WriteLine("Exception Thrown: " + e.Message);
            }


            return "Derp";
        }

    }
}
