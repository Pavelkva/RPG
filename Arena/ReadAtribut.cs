using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Arena
{
    class ReadAtribut
    {

        public int ReadNumber()
        {
            try
            {
                int number = Int32.Parse(Console.ReadLine());
                return number;
            }
            catch
            {
                return 0;
            }
        }
    }
}