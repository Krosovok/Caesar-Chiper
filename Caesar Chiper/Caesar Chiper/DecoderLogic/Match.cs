using Caesar_Chiper.ChiperLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.DecoderLogic
{
    class Match<T>
    {
        private const string FORMAT_TO_STRING = "[{0} ==> {1}] ";
        private char[] symbols;
        private T[] repalaceSymbols;
        private bool[] found;

        public Match(Alphabet alphabet)
        {
            this.symbols = alphabet.Symbols.ToArray();
            repalaceSymbols = new T[symbols.Length];
            found = new bool[symbols.Length];
        }

        public Match(Match<T> other)
        {
            this.symbols = (char[]) other.symbols.Clone();
            int length = other.found.Length;
            this.repalaceSymbols = new T[length];
            this.found = new bool[length];
            this.AddUnknown(other);
        } 

        public T this[char ch]
        {
            get
            {
                int idx = FindIndex(ch);
                if (!found[idx])
                {
                    return default(T);
                }
                else
                {
                    return repalaceSymbols[idx];
                }
            }
            set
            {
                int idx = FindIndex(ch);
                repalaceSymbols[idx] = value;
                found[idx] = true;
            }
        }

        public int FoundCount
        {
            get
            {
                return found.Sum(boolean => boolean ? 1 : 0);
            }
        }

        public static Match<char> RandomMatch(Alphabet alphabet)
        {
            Match<char> newMatch = new Match<char>(alphabet);
            Random random = new Random();

            foreach (char symbol in newMatch.symbols)
            {
                while(newMatch[symbol] == default(char))
                {
                    int randomPos = random.Next(alphabet.AlphabetSize);
                    var randomChar = alphabet.GetChar(randomPos, true);
                    
                    if (!newMatch.repalaceSymbols.Contains(randomChar))
                    {
                        newMatch[symbol] = randomChar;
                    }
                }
            }

            return newMatch;
        }

        public bool AllFound
        {
            get
            {
                return found.All(isFound => isFound);
            }
        }

        public void AddUnknown(Match<T> other)
        {
            int length = other.found.Length;

            for (int i = 0; i < length; i++)
            {
                if (other.found[i])
                {
                    if (this.found[i])
                        //throw new AlreadyHaveMatchException("Already found match.");
                        continue;

                    this.repalaceSymbols[i] = other.repalaceSymbols[i];
                    this.found[i] = true;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < symbols.Length; i++)
            {
                if (found[i])
                    builder.AppendFormat(FORMAT_TO_STRING,
                        symbols[i], repalaceSymbols[i]);
            }

            return builder.ToString();
        }

        private int FindIndex(char c)
        {
            for (int i = 0; i < symbols.Length; i++)
            {
                if (c == symbols[i] ||
                    c == char.ToLower(symbols[i]))
                    return i;
            }

            throw new AlphabetException("No such char in this alphabet.");
        }
    }
}
