using System;
using System.Collections.Generic;
using Acorn;

namespace AcornConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library(@"C:\Users\USERNAME\Music\iTunes\iTunes Music Library.xml");
            List<Song> mySongs = library.initializeLibrary();

            foreach (Song song in mySongs)
            {
                Console.Write("Song Name: {0}\n" +
                              "    Album: {1}\n" +
                              "   Artist: {2}\n\n", song.getAttribute("Name"), song.getAttribute("Album"), song.getAttribute("Artist"));
            }

            Console.ReadLine();
        }
    }
}
