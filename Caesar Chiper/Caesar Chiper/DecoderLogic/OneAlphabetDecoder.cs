using Caesar_Chiper.ChiperLogic;
using Caesar_Chiper.ChiperLogic.Counters;
using Caesar_Chiper.ConsoleDialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.DecoderLogic
{
    class OneAlphabetDecoder
    {
        private Alphabet alphabet;
        private AlphabetCounter overallStatistics;
        private AlphabetCounter encodedStatistics;
        private WordsDictionary dictionary;
        // private Dictionary<char, char> foundMatch = new Dictionary<char, char>();
        private Match<char> foundMatch;

        private const int TAKE_HYPOTHESIS = 5;

        public OneAlphabetDecoder(Alphabet alphabet, string fileName)
        {
            this.alphabet = alphabet;
            overallStatistics = new AlphabetCounter(alphabet);
            encodedStatistics = new AlphabetCounter(alphabet);
            foundMatch = new Match<char>(alphabet);
            overallStatistics.CountInFile(fileName);
            dictionary = new WordsDictionary(fileName);
        }

        public Match<char> Key
        {
            get
            {
                return new Match<char>(foundMatch);
            }
        }

        public void TryDecode(string text)
        {
            encodedStatistics.Add(text);
            Log.Statistics(overallStatistics);
            Log.Statistics(encodedStatistics);
            WordsDictionary dictionary = WordsDictionary.FromString(text);

            List<char> stat = GetStat(overallStatistics);
            List<char> encodedStat = GetStat(encodedStatistics);
            
            foreach (string word in dictionary.Words)
            {
                Log.StartProcessing(word);
                if (foundMatch.AllFound)
                {
                    return;
                }
                
                TryWord(word, stat, encodedStat);
                Log.KnownWords(foundMatch);
            }
            
        }
        
        private List<char> GetStat(AlphabetCounter counter)
        {
            return counter.FrequencyOrderedDescending.Select(pair => pair.Key).ToList();
        }

        private void TryWord(string word, List<char> stat, List<char> encodedStat)
        {
            IEnumerable<char> symbols = word.ToUpper().Distinct();
            Dictionary<char, IEnumerable<char>> hypothesises = 
                new Dictionary<char, IEnumerable<char>>(symbols.Count());

            foreach (char ch in symbols)
            {
                hypothesises.Add(ch, 
                    CreateHypothesis(ch, stat, encodedStat));
            }

            Checker checker = new Checker(
                word,
                alphabet,
                dictionary,
                foundMatch, 
                hypothesises);
            checker.TryBuildWord();
            foundMatch.AddUnknown(checker.Matched);
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

            return stat.GetRange(start, end - start);
        }
    }
}
