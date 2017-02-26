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
        private AlphabetCounter overallStatistics;
        private AlphabetCounter encodedStatistics;
        private FileDictionary dictionary;
        private Dictionary<char, char> foundMatch = new Dictionary<char, char>();

        private const int TAKE_HYPOTHESIS = 5;

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
                if 


                TryWord(word, stat, encodedStat);
            }

           
        }

        private List<char> GetStat(AlphabetCounter counter)
        {
            return counter.FrequencyOrderedDescending.Select(pair => pair.Key).ToList();
        }

        private void TryWord(string word, List<char> stat, List<char> encodedStat)
        {
            IEnumerable<char> symbols = word.Distinct();
            Dictionary<char, IEnumerable<char>> hypothesises = 
                new Dictionary<char, IEnumerable<char>>(symbols.Count());

            foreach (char ch in symbols)
            {
                hypothesises.Add(ch, 
                    CreateHypothesis(ch, stat, encodedStat));
            }

            Checker checker = new Checker(hypothesises, dictionary, word);
            checker.TryBuildWord();
            Dictionary<char, char> found = checker.Matched;
            foreach (KeyValuePair<char, char> match in found)
            {
                foundMatch.Add(match.Key, match.Value);
            }
        }

        private static IEnumerable<char> CreateHypothesis(char symbol, List<char> stat, List<char> encodedStat)
        {
            int idx = encodedStat.IndexOf(symbol);

            int startIdx = idx - TAKE_HYPOTHESIS;
            int endIdx = idx + TAKE_HYPOTHESIS;

            int start = startIdx > 0 ?
                startIdx : 0;
            int end = endIdx < encodedStat.Count ?
                endIdx : encodedStat.Count;

            return stat.GetRange(start, end);
        }
    }
}
