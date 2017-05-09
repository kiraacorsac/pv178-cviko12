using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS2017Showcase
{
    class Program
    {
        static void Main(string[] args)
        {

            string s = "OHGOD THIS IS A REALLY LONG STRING I WOULD REALLY LIKE TO SPLIT IT ACROSS MULTIPLE LINES";
            Console.WriteLine(s);

            //sitrng s = "I CANT INTO ENGLISH"
            //Console.WiteLine(s);
            var reversedString = new Maintenance().GiveString().Reverse();
        }
    }

    public class Maintenance
    {
        public string GiveString() => throw new NotImplementedException();
    }

    public class BugProvider
    {
        public int IncrementNumber(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            else
            {
                if (n % 2 == 1)
                {
                    return 2 * (n / 2) + 1;
                }
                else
                {
                    return n + 1;
                }
            }
        }
    }
}
