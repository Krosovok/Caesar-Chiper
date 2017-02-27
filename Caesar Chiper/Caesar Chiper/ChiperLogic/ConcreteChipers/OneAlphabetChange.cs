using Caesar_Chiper.DecoderLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.ChiperLogic.ConcreteChipers
{
    class OneAlphabetChange : Chiper
    {
        Match<char> key;

        public OneAlphabetChange(IEnumerable<Alphabet> alphabets)
        {
            this.alphabets = alphabets.ToArray();
            Alphabet alphabet = this.alphabets[0];
            int alphabetSize = alphabet.AlphabetSize;

            key = Match<char>.RandomMatch(alphabet);
        }

        public OneAlphabetChange(Match<char> key, IEnumerable<Alphabet> alphabets) 
            : this(alphabets)
        {
            this.key = key;
        }

        protected override char EncodeNext(char toEncode)
        {
            bool isUpper = char.IsUpper(toEncode);
            if (alphabets[0].IsInAlphabet(toEncode))
            {
                char upper = char.ToUpper(toEncode);
                return isUpper ? key[upper] :
                    char.ToLower(key[upper]);
            }
            else
                return toEncode;
        }
    }
}
