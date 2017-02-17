using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.Chiper
{
    abstract class Alphabet //: IEnumerable<char>
    {
        public bool IsInAlphabet(char c)
        {
            return IsInLowerCase(c) ||
                IsInUpperCase(c);
        }

        public abstract char GetChar(int position, bool upperCase);

        public abstract int GetPosition(char c);

        public abstract bool IsInLowerCase(char c);

        public abstract bool IsInUpperCase(char c);

        public abstract int AlphabetSize
        {
            get;
        }

        /// <summary>
        /// Counts addition to position due to signs like 'й' and 'ё'.
        /// </summary>
        protected int CharsBefore(char c, char[] specialChars)
        {
            int afterSpecialSigns = 0;

            foreach (char special in specialChars)
            {
                if (special.CompareTo(c) < 0)
                    afterSpecialSigns++;
            }

            return afterSpecialSigns;
        }
        /*
        public abstract IEnumerator<char> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }*/
    }
}
