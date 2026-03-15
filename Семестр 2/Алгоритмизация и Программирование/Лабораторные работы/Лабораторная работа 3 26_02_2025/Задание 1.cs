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

            Stack st = new Stack();

            bool check = true;

            foreach (char chr in str)
            {
                if (chr == '(' || chr == '[' || chr == '{')
                    st.Push(chr);
                if (chr == ')' || chr == ']' || chr == '}')
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

                    char chr_compare = (char)st.Pop();
                    switch (chr_compare)
                    {
                        case '(':
                            if (chr == ')')
                                continue;
                            else check = false;
                            break;
                        case '[':
                            if (chr == ']')
                                continue;
                            else check = false;
                            break;
                        case '{':
                            if (chr == '}')
                                continue;
                            else check = false;
                            break;
                    }
                    if (check == false)
                    {
                        Console.WriteLine("Математическое выражение введено неверно");
                        break;
                    }
                }
            }

            if (check == true)
            {
                try
                {
                    st.Pop();
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Математическое выражение введено верно - скобки расставлены верно");
                }
                finally
                {
                    if (check == false)
                        Console.WriteLine("Математическое выражение введено неверно");
                }
            }
        }
    }
}
