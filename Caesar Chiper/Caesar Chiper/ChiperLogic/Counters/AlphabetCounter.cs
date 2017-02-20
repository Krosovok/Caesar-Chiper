using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.ChiperLogic.Counters
{
    class AlphabetCounter
    {
        private Alphabet alphabet;
        private Dictionary<char, int> frequency;

        public AlphabetCounter(Alphabet alphabet)
        {
            this.alphabet = alphabet;
            CreateNewDictionary();
        }

        public Dictionary<char, int> Frequency
        {
            get
            {
                return new Dictionary<char, int>(frequency);
            }
        }

        public IOrderedEnumerable<KeyValuePair<char, int>> FrequencyOrderedDescending
        {
            get
            {
                Dictionary<char, int> res = new Dictionary<char, int>(frequency);
                return frequency.OrderByDescending(pair => pair.Value);
            }
        }

        public void Add(string text)
        {
            foreach (char symbol in text.ToUpper())
            {
                if (frequency.Keys.Contains(symbol))
                {
                    frequency[symbol]++;
                }
            }
        }

        public void Clear()
        {
            CreateNewDictionary();
        }
        
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (KeyValuePair<char, int> pair in frequency.OrderByDescending(pair => pair.Value))
            {
                builder.AppendFormat("[{0} ==> {1}]\n", pair.Key, pair.Value);
            }

            return builder.ToString();
        }

        public void CountInFile(string fileName)
        {
            string content = File.ReadAllText(fileName);
            Add(content);
        }

        private void CreateNewDictionary()
        {
            string upperCaseSymbols = alphabet.Symbols;
            frequency = new Dictionary<char, int>();
            foreach (char symbol in upperCaseSymbols)
            {
                frequency.Add(symbol, 0);
            }
        }

    }
}
