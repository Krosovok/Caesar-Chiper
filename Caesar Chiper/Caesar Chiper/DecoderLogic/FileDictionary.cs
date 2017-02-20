using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.DecoderLogic
{
    class FileDictionary
    {
        private string[] words;
        private static char[] SEPARATORS = new char[] { ' ' };

        public FileDictionary(string fileName)
        {
            string content = File.ReadAllText(fileName);
            words = content.Split(SEPARATORS);
        }

        public string Check(string toCheck)
        {
            return words.FirstOrDefault(word => word.Equals(toCheck));
        }
    }
}
