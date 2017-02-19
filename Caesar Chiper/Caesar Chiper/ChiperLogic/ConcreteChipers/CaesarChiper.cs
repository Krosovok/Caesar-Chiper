using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.ChiperLogic
{
    class CaesarChiper : Chiper
    {
        private int key;

        public CaesarChiper(int key, IEnumerable<Alphabet> alphabets)
        {
            this.alphabets = alphabets.ToArray();
            this.key = key;
        }

        protected override char EncodeNext(char toEncode)
        {
            Alphabet alphabet = GetAlphabetWithChar(toEncode);

            if (alphabet == null)
            {
                return toEncode;
            }

            bool upperCase = alphabet.IsInUpperCase(toEncode);
            int newCharPos = (alphabet.GetPosition(toEncode) + key) % alphabet.AlphabetSize;
            if (newCharPos < 0)
            {
                newCharPos = alphabet.AlphabetSize + newCharPos;
            }

            return alphabet.GetChar(newCharPos, upperCase);
        }
    }
}
