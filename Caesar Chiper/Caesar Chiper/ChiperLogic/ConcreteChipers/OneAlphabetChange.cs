using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.ChiperLogic.ConcreteChipers
{
    class OneAlphabetChange : Chiper
    {
        Dictionary<char, char> key;

        public OneAlphabetChange(IEnumerable<Alphabet> alphabets)
        {
            this.alphabets = alphabets.ToArray();
            Alphabet alphabet = this.alphabets[0];
            int alphabetSize = alphabet.AlphabetSize;
            key = new Dictionary<char, char>(alphabetSize);
            Random random = new Random();

            foreach (char symbol in alphabet.Symbols)
            {
                while (!key.ContainsKey(symbol))
                {
                    int randomPos = random.Next(alphabetSize);
                    var randomChar = alphabet.GetChar(randomPos, true);

                    if (!key.ContainsValue(randomChar))
                    {
                        key.Add(symbol, randomChar);
                    }
                }
            }
        }

        /*public void PrintKey()
        {
            foreach (KeyValuePair<char, char> pair in key)
            {
                Console.WriteLine(pair);
            }
        }*/

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
