# Acorn #
A .NET Library for parsing "iTunes Music Library.xml" files

####To Build:
- Compile in Visual Studio


####To Use:
- Add as a reference to your project.


####Example:

    public static void main(string[] args)
    {
        Library library = new Library(@"C:\User\Foo\Music\iTunes\iTunes Music Library.xml");
        List<Song> mySongs = library.parseSongs();
    }