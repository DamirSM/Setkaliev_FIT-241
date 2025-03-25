using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите математическое выражение:");
            string str = Console.ReadLine();
            string[] elems = str.Split(' ');

            Stack st = new Stack();
            int[] res_arr = new int[2];

            bool check = true;

            foreach (string elem in elems)
            {
                if (elem != "+" && elem != "-" && elem != "*" && elem != "/")
                {
                    res_arr[0] = Convert.ToInt32(elem);
                    st.Push(elem);
                }
                if (elem == "+" || elem == "-" || elem == "*" || elem == "/")
                {
                    for (int i = 0; i < 2; i++)
                    {
                        try
                        {
                            st.Peek();
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine("Математическое выражение введено неверно");
                            check = false;
                            break;
                        }
                            res_arr[i] = Convert.ToInt32(st.Pop());
                    }

                    if (check == false)
                        break;


                    switch (elem)
                    {
                        case "+":
                            res_arr[0] = res_arr[1] + res_arr[0];
                            break;
                        case "-":
                            res_arr[0] = res_arr[1] - res_arr[0];
                            break;
                        case "*":
                            res_arr[0] = res_arr[1] * res_arr[0];
                            break;
                        case "/":
                            if (res_arr[0] == 0)
                            {
                                Console.WriteLine("Математическое выражение введено неверно");
                                check = false;
                            }
                            else
                                res_arr[0] = res_arr[1] / res_arr[0];
                            break;
                    }
                    st.Push(res_arr[0]);
                }
            }

            if (check == true)
            {
                try
                {
                    st.Peek();
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Математическое выражение введено неверно");
                }
                finally
                {
                    if (st.Count != 1)
                        Console.WriteLine("Математическое выражение введено неверно");
                    else
                        Console.WriteLine(res_arr[0]);
                }
            }
        }
    }
}
