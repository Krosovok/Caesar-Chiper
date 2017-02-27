using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.DecoderLogic
{
    class WordsDictionary
    {
        private string[] words;
        private Dictionary<int, List<string>> wordsDictionary;
        private static char[] SEPARATORS = new char[] { ' ' };

        public string[] Words
        {
            get
            {
                return words.ToArray();
            }
        }

        public WordsDictionary(string fileName)
        {
            string content = File.ReadAllText(fileName);
            SetWords(content);
        }

        private WordsDictionary() { }

        public string Check(string toCheck)
        {
            return wordsDictionary[toCheck.Length].
                FirstOrDefault(str => str.Equals(toCheck));
        }

        public static WordsDictionary FromString(string str)
        {
            WordsDictionary newWords = new WordsDictionary();
            newWords.SetWords(str);
            return newWords;
        }

        private void SetWords(string str)
        {
            words = str.Split(SEPARATORS).Where(word => !string.IsNullOrWhiteSpace(word)).ToArray();
            IEnumerable<IGrouping<int, string>> grouped = 
                words.GroupBy(s => s.Length);
            wordsDictionary = new Dictionary<int, List<string>>();

            foreach (IGrouping<int, string> group in grouped)
            {
                wordsDictionary.Add(group.Key, group.Select(s => s.ToUpper()).ToList());
            }
        }
    }
}
