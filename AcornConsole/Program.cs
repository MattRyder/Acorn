using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acorn;

namespace AcornConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Library lib = new Library(@"C:\Users\Matt\Documents\iTunes Music Library.xml");
            lib.initializeLibrary();
        }
    }
}
