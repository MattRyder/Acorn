# Acorn
A .NET Library for parsing "iTunes Music Library.xml" files
(https://github.com/MattRyder/Acorn)

####To Build:
- Open Acorn.sln and compile in Visual Studio.


####To Use:
- Add as a reference to your project. (Right Click > Add Reference...)
- (optional) Add 'Acorn' namespace to the project. ("using Acorn;")


####Examples:
    
    public static void main(string[] args)
    {
        Library library = new Library(@"C:\User\Foo\Music\iTunes\iTunes Music Library.xml");
        List<Song> mySongs = library.parseSongs();
    }