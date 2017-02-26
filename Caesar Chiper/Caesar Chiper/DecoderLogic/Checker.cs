using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.DecoderLogic
{
    // TO DO: Skip known substitutions.
    class Checker
    {
        private Dictionary<char, IEnumerable<char>> hypothesises;
        private IEnumerable<char> symbols;
        private FileDictionary knownWords;
        private Dictionary<char, char> matched;
        private string encodedWord;

        public Checker(Dictionary<char, IEnumerable<char>> hypothesises,
            FileDictionary knownWords, string word)
        {
            encodedWord = word;
            this.hypothesises = hypothesises;
            this.knownWords = knownWords;

            symbols = hypothesises.Keys;
        }

        public void TryBuildWord()
        {
            BuildWord(new Dictionary<char, char>(), 0);
        }

        public Dictionary<char, char> Matched
        {
            get
            {
                if (matched == null)
                    return new Dictionary<char, char>();
                else
                    return new Dictionary<char, char>(matched);
            }
        }

        private void BuildWord(Dictionary<char, char> assumption, int idx)
        {
            char nextChar = symbols.ElementAtOrDefault(idx);

            if (nextChar != default(char)) //??? 
            {
                IEnumerable<char> nextCharHypothesis = hypothesises[nextChar];
                foreach (char hypothetic in nextCharHypothesis)
                {
                    Dictionary<char, char> nextAssumption = new Dictionary<char, char>(assumption);
                    nextAssumption.Add(nextChar, hypothetic);
                    BuildWord(nextAssumption, idx + 1);
                }
            }
            else
            {
                Compare(assumption);
            }
        }

        private void Compare(Dictionary<char, char> assumption)
        {
            string word = new string(
                encodedWord.Select(ch => assumption[ch]).ToArray());

            string found = knownWords.Check(word);

            if (found != null)
            {
                if (matched != null)
                    throw new AlreadyHaveMatchException("Already found match on this sequence of charcters.");

                matched = assumption;
            }
        }
    }
}
