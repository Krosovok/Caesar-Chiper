using Caesar_Chiper.ChiperLogic;
using Caesar_Chiper.ConsoleDialog;
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
        private WordsDictionary knownWords;
        // private Dictionary<char, char> matched;
        private Match<char> matched;
        private string encodedWord;
        private Alphabet alphabet;
        private Match<char> alreadyKnown;

        public Checker(string word,
            Alphabet alphabet,
            WordsDictionary knownWords,
            Match<char> alreadyFound,
            Dictionary<char, IEnumerable<char>> hypothesises)
        {
            encodedWord = word;
            this.hypothesises = hypothesises;
            this.knownWords = knownWords;
            this.alphabet = alphabet;
            this.alreadyKnown = alreadyFound;

            symbols = hypothesises.Keys.Where(symbol => alphabet.IsInAlphabet(symbol));
        }

        public void TryBuildWord()
        {
            try
            {
                BuildWord(new Match<char>(alphabet), 0);
            }
            catch (AlreadyHaveMatchException e)
            {
                this.matched = null;
                Log.Error(e.Message);
            }
        }

        public Match<char> Matched
        {
            get
            {
                if (matched == null)
                    return new Match<char>(alphabet);
                else
                    return new Match<char>(matched);
            }
        }

        private void BuildWord(Match<char> assumption, int idx)
        {
            char nextChar = symbols.ElementAtOrDefault(idx);

            if (nextChar != default(char))
            {
                if (alreadyKnown[nextChar] == default(char))
                {
                    AssumeNext(assumption, idx, nextChar);
                }
                else
                {
                    NextAlreadyFound(assumption, idx, nextChar);
                }

            }
            else
            {
                Compare(assumption);
            }
        }

        private void NextAlreadyFound(Match<char> assumption, int idx, char nextChar)
        {
            assumption[nextChar] = alreadyKnown[nextChar];
            BuildWord(assumption, idx + 1);
        }

        private void AssumeNext(Match<char> assumption, int idx, char nextChar)
        {
            IEnumerable<char> nextCharHypothesis = hypothesises[nextChar];
            foreach (char hypothetic in nextCharHypothesis)
            {
                Match<char> nextAssumption = new Match<char>(assumption);
                nextAssumption[nextChar] = hypothetic;
                BuildWord(nextAssumption, idx + 1);
            }
        }

        private void Compare(Match<char> assumption)
        {
            string word = new string(
                encodedWord.Select(
                    ch =>
                    alphabet.IsInAlphabet(ch) ?
                    assumption[ch] : 
                    ch
                ).ToArray());

            string found = knownWords.Check(word);

            if (found != null)
            {
                if (matched != null)
                    throw new AlreadyHaveMatchException("Already found match on this sequence of charcters.");

                matched = assumption;
                Log.FoundWord(encodedWord, found);
            }
        }
    }
}
