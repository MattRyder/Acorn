using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acorn
{
    public class Library
    {
        LibraryParser parser;
        string libraryLocation;

        public string LibraryLocation
        {
            get { return libraryLocation; }
            set { libraryLocation = value; }
        }

        public Library(string libraryLocation)
        {
            this.libraryLocation = libraryLocation;
            parser = new LibraryParser(libraryLocation);
        }

        public List<Song> parseSongs()
        {
            if (libraryLocation != string.Empty)
                return parser.getSongsFromLibrary();
            else 
                return null;
        }
    }
}
