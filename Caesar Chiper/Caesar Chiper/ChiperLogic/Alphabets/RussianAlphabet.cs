using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.ChiperLogic
{
    class RussianAlphabet : SimpleAlphabet
    {
        private const char SPECIAL_RUSSIAN_LOWER_CASE = 'ё';
        private const char SPECIAL_RUSSIAN_UPPER_CASE = 'Ё';

        /// <summary>
        /// If a sign is after one of this it is also after special symbol 'ё'.
        /// </summary>
        private static readonly char[] specialLowerCase = { 'е' };
        private static readonly char[] specialUpperCase = { 'Е' };

        public override char[] SpecialLowerCase
        {
            get
            {
                return specialLowerCase;
            }
        }

        public override char[] SpecialUpperCase
        {
            get
            {
                return specialUpperCase;
            }
        }

        public override char FIRST_LOWER_CASE
        {
            get
            {
                return 'а';
            }
        }

        public override char LAST_LOWER_CASE
        {
            get
            {
                return 'я';
            }
        }

        public override char FIRST_UPPER_CASE
        {
            get
            {
                return 'А';
            }
        }

        

        public char LAST_UPPER_CASE { get { return 'Я'; } }

        public override bool IsInLowerCase(char c)
        {
            return FIRST_LOWER_CASE <= c && c <= LAST_LOWER_CASE ||
                SPECIAL_RUSSIAN_LOWER_CASE.Equals(c);
        }

        public override bool IsInUpperCase(char c)
        {
            return FIRST_UPPER_CASE <= c && c <= LAST_UPPER_CASE ||
                SPECIAL_RUSSIAN_UPPER_CASE.Equals(c);
        }

        protected override int GetSpecialCharPosition(char c)
        {
            switch (c)
            {
                case SPECIAL_RUSSIAN_LOWER_CASE:
                    return specialLowerCase[0] - FIRST_LOWER_CASE + 1;
                case SPECIAL_RUSSIAN_UPPER_CASE:
                    return specialUpperCase[0] - FIRST_UPPER_CASE + 1;
                default:
                    return -1;
            }
        }

        protected override char GetSpecialChar(int position, bool upperCase)
        {
            if (position == GetSpecialCharPosition(SPECIAL_RUSSIAN_LOWER_CASE))
            {
                return upperCase ? SPECIAL_RUSSIAN_UPPER_CASE : SPECIAL_RUSSIAN_LOWER_CASE;
            }
            else
            {
                return NO_CHAR;
            }
        }
    }
}
