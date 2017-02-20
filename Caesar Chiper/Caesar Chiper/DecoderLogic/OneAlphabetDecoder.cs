using Caesar_Chiper.ChiperLogic;
using Caesar_Chiper.ChiperLogic.Counters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.DecoderLogic
{
    class OneAlphabetDecoder
    {
        AlphabetCounter overallStatistics;
        AlphabetCounter encodedStatistics;
        FileDictionary dictionary;

        public OneAlphabetDecoder(Alphabet alphabet, string fileName)
        {
            overallStatistics = new AlphabetCounter(alphabet);
            encodedStatistics = new AlphabetCounter(alphabet);
            overallStatistics.CountInFile(fileName);
            dictionary = new FileDictionary(fileName);
        }

        public void TryDecode(string text)
        {
            encodedStatistics.Add(text);
            string[] words = text.Split(' ');

            List<char> stat = GetStat(overallStatistics);
            List<char> encodedStat = GetStat(encodedStatistics);

            foreach (string word in words)
            {
                foreach (char symbol in word)
                {

                }
            }

           
        }

        private List<char> GetStat(AlphabetCounter counter)
        {
            return counter.FrequencyOrderedDescending.Select(pair => pair.Key).ToList();
        }

        private void TryWord(string word, List<char> stat, List<char> encodedStat)
        {
            IEnumerable<char> symbols = word.Distinct();

            int idx = encodedStat.IndexOf(symbols.ElementAt(0));

            /*for (int i = 0; i < encodedStat.Length; i++)
            {
                for (int j = start; j < end; j++)
                {

                }
            }*/
        }
    }
}
