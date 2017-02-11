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
            IAlphabet russian = new RussianAlphabet();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(russian.GetChar(i, true));
            }

            Console.ReadLine();
        }
    }
}
