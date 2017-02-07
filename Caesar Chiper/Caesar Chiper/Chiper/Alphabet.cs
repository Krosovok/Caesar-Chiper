using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.Chiper
{
    abstract class IAlphabet
    {
        public bool IsInAlphabet(char c)
        {
            return IsInLowerCase(c) ||
                IsInUpperCase(c);
        }

        public abstract char GetChar(int position);

        public abstract int GetPosition(char c);

        protected abstract bool IsInLowerCase(char c);

        protected abstract bool IsInUpperCase(char c);

        /// <summary>
        /// Counts addition to position due to signs like 'й' and 'ё'.
        /// </summary>
        protected int CharsBefore(char c, char[] specialChars)
        {
            int afterSpecialSigns = 0;

            foreach (char special in specialChars)
            {
                if (special.CompareTo(c) > 0)
                    afterSpecialSigns++;
            }

            return afterSpecialSigns;
        }
    }
}
