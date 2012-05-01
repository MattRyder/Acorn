using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acorn;

namespace Acorn
{
    public class Album
    {
        string name;
        List<Song> songs;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Song> Songs
        {
            get { return songs; }
            set { songs = value; }
        }

        public Album(string name)
        {
            this.name = name;
        }

        public Album(string name, Song song)
        {
            this.name = name;
            this.songs = new List<Song>();
            this.songs.Add(song);
        }

        public Album(string name, List<Song> songs)
        {
            this.name = name;
            this.songs = songs;
        }
    }
}
