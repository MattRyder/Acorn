using System.Collections.Generic;

namespace Acorn.iTunes
{
    /// <summary>
    /// Represents an iTunes Library, containing media such as Song
    /// </summary>
    public class Library
    {
        LibraryParser parser;
        List<Song> songs;

        string libraryLocation;

        public string LibraryLocation
        {
            get { return libraryLocation; }
            set { libraryLocation = value; }
        }

        public List<Song> Songs
        {
            get { return songs; }
            set { songs = value; }
        }

        public Library(string libraryLocation)
        {
            this.libraryLocation = libraryLocation;
            parser = new LibraryParser(libraryLocation);
        }

        public List<Song> initializeLibrary()
        {
            if (libraryLocation != string.Empty)
            {
                return parser.parse();
            }
            else return null;
        }
    }
}
