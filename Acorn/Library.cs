using System.Collections.Generic;

namespace Acorn
{
    /// <summary>
    /// Represents an iTunes Library, containing media such as Songs or Video
    /// </summary>
    public class Library
    {
        LibraryParser parser;
        List<Album> albums;

        string libraryLocation;

        public string LibraryLocation
        {
            get { return libraryLocation; }
            set { libraryLocation = value; }
        }

        public List<Album> Albums
        {
            get { return albums; }
            set { albums = value; }
        }

        public Library(string libraryLocation)
        {
            this.libraryLocation = libraryLocation;
            parser = new LibraryParser(libraryLocation);
        }

        public bool initializeLibrary()
        {
            string songAlbumName;
            bool isAdded = false;

            albums = new List<Album>();

            if (libraryLocation != string.Empty)
            {
                List<Song> songs = parser.parse();

                foreach (Song song in songs)
                {
                    isAdded = false;

                    //If the song has an album name, use it. Otherwise, put them all in "Unknown Album"
                    if ((songAlbumName = (string)song.getAttribute("Album")) == null)
                        songAlbumName = "Unknown Album";

                    foreach (Album album in albums)
                    {
                        if (album.Name == songAlbumName)
                        {
                            album.Songs.Add(song);
                            isAdded = true;
                            break;
                        }
                    }

                    if (!isAdded)
                    {
                        //Album doesn't exist, create new album for song:
                        albums.Add(new Album(songAlbumName, song));
                    }
                }
                return true;

            }
            else return false;

        }
    }
}
