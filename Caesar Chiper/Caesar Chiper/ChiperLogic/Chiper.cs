using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.ChiperLogic
{
    abstract class Chiper
    {
        protected Alphabet[] alphabets;
        
        public string Encode(string message)
        {
            StringBuilder builder = new StringBuilder(message.Length);

            foreach (char toEncode in message)
            {
                builder.Append(
                    EncodeNext(toEncode));
            }

            return builder.ToString();
        }

        protected Alphabet GetAlphabetWithChar(char toEncode)
        {
            return alphabets.FirstOrDefault(x => x.IsInAlphabet(toEncode));
        }

        protected abstract char EncodeNext(char toEncode);
    }
}
