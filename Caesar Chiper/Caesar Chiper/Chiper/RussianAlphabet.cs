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
        /// If a sign is after one of this it is also after special symbols 'й' and 'ё'.
        /// </summary>
        private static readonly char[] specialLowerCase = { 'е', 'и' };
        private static readonly char[] specialUpperCase = { 'Е', 'И' };

        public char GetChar(int position)
        {
        }


        public int GetPosition(char c)
        {

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

        protected bool IsInLowerCase(char c)
        {
            return 'а' < c && c < 'я' ||
                'ё'.Equals(c) || 'й'.Equals(c);
        }

        protected bool IsInUpperCase(char c)
        {
            return 'А' < c && c < 'Я' ||
                'Ё'.Equals(c) || 'Й'.Equals(c);
        }

        private int GetSpecialCharsPosition(char c)
        {
            switch (c)
            {
                case 'й':
                    return 'и' + 1;
                case 'Й':
                    return 'И' + 1;
                case 'ё':
                    return 'е' + 1;
                case 'й':
                    return 'и' + 1;
            }
        }
    }
}
