using System;

namespace StageOne
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathRead = @"Example Configuration File\1.ini";
            string pathWrite = @"Example Configuration File\2.ini";
            SortingInitializationFile.Alphabetically(pathRead, pathWrite);            
        }
    }
}
