# Acorn
A .NET Library for parsing "iTunes Music Library.xml" files
(https://github.com/MattRyder/Acorn)

####To Build:
- Open Acorn.sln and compile in Visual Studio.
- Output build can be found in /bin/Release


####To Use:
- Add as a reference to your project. (Right Click > Add Reference...)
- (optional) Add 'Acorn' namespace to the project. ("using Acorn;")


####Examples:
    
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