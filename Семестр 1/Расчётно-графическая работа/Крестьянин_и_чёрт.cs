using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int MaxN = Convert.ToInt32(Console.ReadLine());
            if (1 <= MaxN && MaxN <= 200000000)
            {
                int count = 0;
                // Console.WriteLine("N\tK\tZ");
                for (int N = 1; N <= MaxN; N++)
                {
                    int n = N*2;
                    for (int K = N+1; K <= n; K++)
                    {
                        int Z = 1;
                        int temp = n;
                        while (temp > 0)
                        {
                            temp -= K;
                            if (temp == 0)
                            {
                                // Console.WriteLine(N+"\t"+K+"\t"+Z);
                                count += 1;
                            }
                            Z++;
                            temp *= 2;
                        }
                    }
                }
                Console.WriteLine(count);
            }
        }
    }
}
