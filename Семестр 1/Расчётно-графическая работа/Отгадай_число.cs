using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = Convert.ToInt32(Console.ReadLine());
            if (0 <= N && N <= 100)
            {
                string[] equation = new string[N];
                int x = 1;
                int sum = 0;
                for (int i = 0; i < N; i++)
                {
                    string var = "";
                    string SV = Console.ReadLine().ToLower();
                    string[] S_V = SV.Split(new char[] { ' ' });
                    string S = S_V[0];
                    string V = S_V[1];
                    if (V == "x")
                    {
                        switch (S)
                        {
                            case "+":
                                var = "+x";
                                x += 1;
                                break;
                            case "-":
                                var = "-x";
                                x -= 1;
                                break;
                        }
                    }
                    else
                    {
                        int num = Convert.ToInt32(V);
                        if (Math.Abs(num) <= 100)
                        {
                            switch(S)
                            {
                                case "+":
                                    var = "+" + V;
                                    sum += num;
                                    break;
                                case "-":
                                    var = "-" + V;
                                    sum -= num;
                                    break;
                                case "*":
                                    var = "*" + V;
                                    sum *= num;
                                    x *= num;
                                    break;
                            }
                        }
                    }
                    Console.WriteLine(x + "x + " + sum);
                    equation[i] = var;
                }
                int R = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(string.Join(" ", equation));
                R -= sum;
                x = R / x;
                Console.WriteLine(x);
            }
        }
    }
}
