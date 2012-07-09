using System;
using System.Collections.Generic;
using Acorn.iTunes;
using Acorn.ID3;
using System.IO;

namespace AcornConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ID3Parser parser = new ID3Parser();

            Stream stream = File.OpenRead(@"C:\Users\Public\Music\Sample Music\Kalimba.mp3");
            parser.parseHeader(stream);

            Console.ReadLine();
        }
    }
}
