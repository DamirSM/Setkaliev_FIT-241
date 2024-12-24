using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            double dif = n;
            int count = 0;
            while (n > 3)
            {
                dif /= 2;
                n /= 2;
                count++;
            }
            dif = dif - n;
            Console.WriteLine(count + "\n" + dif);
            double pow = Math.Pow(2, count);
            switch (n)
            {
                case 2:
                    Console.WriteLine(dif * pow);
                    break;
                case 3:
                    Console.WriteLine(pow - dif * pow);
                    break;
            }
        }
    }
}