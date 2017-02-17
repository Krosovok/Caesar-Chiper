using Caesar_Chiper.Chiper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.ConsoleDialog
{
    class Test
    {
        public static void Run()
        {
            Alphabet russian = new RussianAlphabet();

            for (int i = 0; i < russian.AlphabetSize; i++)
            {
                char c = russian.GetChar(i, true);
                int pos = russian.GetPosition(c);
                Console.WriteLine("{0} ({1})", c, pos);
            }

            Console.ReadLine();
        }
    }
}
