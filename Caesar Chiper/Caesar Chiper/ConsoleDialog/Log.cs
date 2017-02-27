using Caesar_Chiper.ChiperLogic.Counters;
using Caesar_Chiper.DecoderLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.ConsoleDialog
{
    static class Log
    {
        private const string FOUND_WORD = "Found: {0} => {1}\t\t";
        private const string COUNT = "Known symbols: {0}";
        private const string ERROR = "Error: {0}";

        public static void FoundWord(string encodedWord, string word)
        {
            Console.Write(FOUND_WORD, encodedWord, word);
        }

        public static void KnownWords(Match<char> foundMatch)
        {
            Console.WriteLine(COUNT, foundMatch.FoundCount);
        }

        public static void Error(string message)
        {
            Console.WriteLine(ERROR, message);
        }

        public static void StartProcessing(string word)
        {
            Console.WriteLine("Start processing {0}...", word);
        }

        public static void Statistics(AlphabetCounter counter)
        {
            Console.WriteLine(counter);
        }
    }
}
