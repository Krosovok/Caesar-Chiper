using Caesar_Chiper.ChiperLogic.Counters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.ConsoleDialog
{
    class DataPresenter
    {
        public string ToDiagram(AlphabetCounter counter)
        {
            Dictionary<char, int> frequency = counter.Frequency;
            var ordered = frequency.OrderByDescending(pair => pair.Value);

            StringBuilder builder = new StringBuilder();

            foreach (KeyValuePair<char, int> pair in ordered)
            {
                if (pair.Value == 0)
                    continue;

                builder.Append(pair.Key);
                builder.Append(" ");
                for (int i = 0; i < pair.Value; i++)
                {
                    builder.Append("|");
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
