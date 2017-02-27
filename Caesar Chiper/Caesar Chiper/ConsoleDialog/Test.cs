using Caesar_Chiper.ChiperLogic;
using Caesar_Chiper.ChiperLogic.Concrete;
using Caesar_Chiper.ChiperLogic.ConcreteChipers;
using Caesar_Chiper.ChiperLogic.Counters;
using Caesar_Chiper.DecoderLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.ConsoleDialog
{
    class Test
    {
        private const string PATH = "..\\..\\";
        private const string SAMPLE_TEXT = "Этот текст будет зашифрован и расшифрован. А потом в нём посчитают буквы!";
        private const string RAVEN_ANNOUNCE = "Стихотворение \"Ворон\":";
        private const string RAVEN_TXT = PATH + "Ворон.txt";
        private const string CS_TYPES = PATH + "Операторы C#.txt";
        private const string CS_TYPES_ANNOUNCE = "Операторы в C# с msdn:";
        private const string PARABLE = PATH + "Притча о блудном сыне.txt";
        private const string PARABEL_ANNOUNCE = "Притча о блудном сыне:";
        private const string _20000 = PATH + "20000.txt";
        private const int TEST_KEY = 11;
        private const int TEST_1 = 56515241;
        private const int TEST_2 = 274936615;
        private const int ALL_LINES = -1;
        private const int THREE_CHAPTERS = 263;
        private static Alphabet[] alphabets = {
                new RussianAlphabet()
            };

        public static void Alphabet()
        {
            Alphabet russian = new RussianAlphabet();

            for (int i = 0; i < russian.AlphabetSize; i++)
            {
                char c = russian.GetChar(i, true);
                int pos = russian.GetPosition(c);
                Console.WriteLine("{0} ({1})", c, pos);
            }

            string upperCase = russian.Symbols;
            Console.WriteLine(upperCase);
            string lowerCase = upperCase.ToLower();
            Console.WriteLine(lowerCase);

            Console.ReadLine();
        }

        public static void CaesarEncode()
        {
            Chiper encoder = new CaesarChiper(TEST_KEY, alphabets);
            Chiper decoder = new CaesarChiper(-TEST_KEY, alphabets);
            string message = SAMPLE_TEXT;
            
            Encode(message, encoder, decoder);
            
        }

        private static void Encode(string message, Chiper encoder, Chiper decoder)
        {
            Console.WriteLine(message);

            string encoded = encoder.Encode(message);
            Console.WriteLine(encoded);

            string decoded = decoder.Encode(encoded);
            Console.WriteLine(decoded);
            
            Console.ReadLine();
        }

        public static void Counting()
        {
            Alphabet russian = new RussianAlphabet();
            AlphabetCounter counter = new AlphabetCounter(russian);

            counter.Add(SAMPLE_TEXT);

            Console.WriteLine(counter);
            Console.WriteLine(
                new DataPresenter().ToDiagram(counter));
            counter.Clear();
            Console.ReadLine();

            CountInFile(counter, RAVEN_TXT, RAVEN_ANNOUNCE);
            CountInFile(counter, CS_TYPES, CS_TYPES_ANNOUNCE);
            CountInFile(counter, PARABLE, PARABEL_ANNOUNCE);
        }

        public static void NotepadEncode()
        {
            string key = File.ReadAllText(RAVEN_TXT);
            Chiper encoder = new OneTimePad(key, alphabets);
            Chiper decoder = new OneTimePad(key, alphabets);
            string message = SAMPLE_TEXT;

            Encode(message, encoder, decoder);
        }

        public static void Xor()
        {
            int myRes = XOR.Operation(TEST_1, TEST_2);
            int res = (TEST_1 ^ TEST_2);

            bool working = res == myRes;

            Console.WriteLine(res);
            Console.WriteLine(myRes);
            Console.WriteLine((working ? "" : "Not ")
                + "Working!");
        }

        public static void Count2()
        {
            Alphabet russian = new RussianAlphabet();
            AlphabetCounter counter = new AlphabetCounter(russian);

            CountInFile(counter, PATH + "Шифр.txt", "");

            string message = File.ReadAllText(PATH + "Шифр.txt");
            //Chiper caesar = new CaesarChiper(10, alphabets);
            Chiper caesar = new CaesarChiper(16, alphabets);
            string res = caesar.Encode(message);
            Console.WriteLine(res);
            Console.ReadLine();
        }

        public static void OneAlphabetChange()
        {
            Chiper oneAlphabet = 
                new OneAlphabetChange(
                    alphabets);

            //string allText = File.ReadAllText(RAVEN_TXT);
            string encodedPart = EncodeFromFile(oneAlphabet, _20000, THREE_CHAPTERS);

            OneAlphabetDecoder decoder = 
                new OneAlphabetDecoder(
                    new RussianAlphabet(),
                    _20000);
            decoder.TryDecode(encodedPart);
            Match<char> key = decoder.Key;
            Console.WriteLine("Finished processing. Found key: {0}", key);
            Console.ReadLine();

            Chiper reader = new OneAlphabetChange(key, alphabets);
            string decodedPart = reader.Encode(encodedPart);
            Console.WriteLine(decodedPart);
            Console.ReadLine();
        }

        private static void CountInFile(AlphabetCounter counter, string fileName, string announce)
        {
            Console.WriteLine(announce);
            counter.CountInFile(fileName);
            Console.WriteLine(counter);
            counter.Clear();
            Console.ReadLine();
        }

        private static string EncodeFromFile(Chiper chiper, string fileName, int lines = ALL_LINES)
        {
            string[] content = File.ReadAllLines(fileName);
            if (lines == ALL_LINES)
            {
                lines = content.Length;
            }

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < lines; i++)
            {
                builder.AppendLine(
                    chiper.Encode(content[i]));
            }
            string encoded = builder.ToString();

            Console.WriteLine(encoded);
            Console.ReadLine();
            return encoded;
        }
    }
}
