using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caesar_Chiper.ChiperLogic
{
    class XOR
    {
        public static int Operation(int a, int b)
        {
            //return ~((a & b) | ~(a | b)); 
            return ~(a & b) & (a | b);
        } 
    }
}
