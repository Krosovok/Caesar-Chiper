using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caesar_Chiper.Chiper
{
    class AlphabetException : Exception
    {
        public AlphabetException(string message) : base(message) { }
    }
}
