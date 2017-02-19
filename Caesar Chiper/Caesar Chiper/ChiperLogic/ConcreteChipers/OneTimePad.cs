using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.ChiperLogic.Concrete
{
    class OneTimePad : Chiper
    {
        private string key;
        private int keyLength;
        private int pos = 0;

        public OneTimePad(string key, IEnumerable<Alphabet> alphabets)
        {
            this.alphabets = alphabets.ToArray();

            this.key = key;
            keyLength = key.Length;
        }
        
        protected override char EncodeNext(char toEncode)
        {
            char keyChar = GetKey();

            return (char)(toEncode ^ keyChar);
        }

        private char GetKey()
        {
            Alphabet alphabet = null;
            char keyChar = Alphabet.NO_CHAR;
            while (alphabet == null)
            {
                keyChar = key[pos++ % keyLength];
                alphabet = GetAlphabetWithChar(keyChar);
            }

            return keyChar;
        }
        
    }
}
