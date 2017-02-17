using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.Chiper
{
    abstract class SimpleAlphabet : Alphabet
    {
        public abstract char[] SpecialLowerCase { get; }
        public abstract char[] SpecialUpperCase { get; }
        public abstract char FIRST_LOWER_CASE { get; }
        public abstract char LAST_LOWER_CASE { get; }
        public abstract char FIRST_UPPER_CASE { get; }
        public abstract char NONE { get; }

        public override int AlphabetSize
        {
            get
            {
                return LAST_LOWER_CASE - FIRST_LOWER_CASE + SpecialLowerCase.Length + 1;
            }
        }


        public override char GetChar(int position, bool upperCase)
        {
            if (position > AlphabetSize || position < 0)
                throw new AlphabetException("Out of range of the alphabet.");

            char[] special = upperCase ?
                SpecialUpperCase : SpecialLowerCase;
            char firstChar = upperCase ? FIRST_UPPER_CASE : FIRST_LOWER_CASE;

            char spec = GetSpecialChar(position, upperCase);
            if (spec != NONE)
            {
                return spec;
            }

            char baseChar = (char)(firstChar + position);
            int charsBefore = CharsBefore(baseChar, special);
            return (char)(baseChar - charsBefore);
        }

        /// <summary>
        /// Returns position of the char in the alphabet.
        /// </summary>
        /// <returns>A number representing char's position in the alphabet.</returns>
        public override int GetPosition(char c)
        {
            int specialCharPos = GetSpecialCharPosition(c);
            if (specialCharPos != -1)
            {
                return specialCharPos;
            }

            if (IsInLowerCase(c))
            {
                return c - FIRST_LOWER_CASE + CharsBefore(c, SpecialLowerCase);
            }
            else if (IsInUpperCase(c))
            {
                return c - FIRST_UPPER_CASE + CharsBefore(c, SpecialUpperCase);
            }
            else
            {
                throw new AlphabetException("No such char in this alphabet.");
            }
        }

        protected abstract int GetSpecialCharPosition(char c);

        protected abstract char GetSpecialChar(int position, bool upperCase);
    }
}
