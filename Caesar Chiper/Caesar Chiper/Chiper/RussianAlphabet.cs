using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.Chiper
{
    class RussianAlphabet : IAlphabet
    {
        /*public bool IsInAlphabet(char c)
        {
            return 
                'А' < c && c < 'Я' ||
                'Ё'.Equals(c) ||
                'а' < c && c < 'я' ||
                'ё'.Equals(c);
        }*/

        /// <summary>
        /// If a sign is after one of this it is also after special symbols 'ё'.
        /// </summary>
        private static readonly char[] specialLowerCase = { 'е' };
        private static readonly char[] specialUpperCase = { 'Е' };

        public override char GetChar(int position, bool upperCase)
        {
            if (position > 'а' - 'я' + specialLowerCase.Length || position < 0)
                throw new AlphabetException("Out of range of the alphabet.");

            char[] special = upperCase ?
                specialUpperCase : specialLowerCase;
            char firstChar = upperCase ? 'А' : 'а';

            char spec = GetSpecialChar(position, upperCase);
            if (spec != '\0')
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
                return c - 'а' + CharsBefore(c, specialLowerCase);
            }
            else if (IsInUpperCase(c))
            {
                return c - 'А' + CharsBefore(c, specialUpperCase);
            }
            else
            {
                throw new AlphabetException("No such char in this alphabet.");
            }
        }

        public override bool IsInLowerCase(char c)
        {
            return 'а' < c && c < 'я' ||
                'ё'.Equals(c);
        }

        public override bool IsInUpperCase(char c)
        {
            return 'А' < c && c < 'Я' ||
                'Ё'.Equals(c);
        }

        private int GetSpecialCharPosition(char c)
        {
            switch (c)
            {
                case 'ё':
                    return 'е' - 'а' + 1;
                case 'Ё':
                    return 'Е' - 'А' + 1;
                default:
                    return -1;
            }
        }

        private char GetSpecialChar(int position, bool upperCase)
        {
            if (position == GetSpecialCharPosition('ё'))
            {
                return upperCase ? 'Ё' : 'ё';
            }
            else
            {
                return '\0';
            }
        }
    }
}
