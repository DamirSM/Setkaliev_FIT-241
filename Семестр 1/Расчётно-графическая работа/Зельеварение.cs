using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество действий: ");
            int N = Convert.ToInt32(Console.ReadLine());
            string[] actions = new string[N];
            for (int i = 0; i < N; i++)
            {
                Console.Write("\nВведите действие и через пробел необходимые ингредиенты: ");
                actions[i] = Console.ReadLine();
            }
            for (int i = 0; i < N; i++)
            {
                string action = actions[i];
                string[] details = action.Split(new char[] { ' ' });
                int len = details.GetLength(0);
                int number;
                for (int j = 0; j < len; j++)
                {
                    if (Int32.TryParse(details[j], out number))
                    {
                        number--;
                        details[j] = "";
                        if (number < i)
                        {
                            string new_detail = string.Join("", actions[number]);
                            details[j] = new_detail;
                        }
                    }
                }
                switch (details[0])
                {
                    case "MIX":
                        details[0] = "";
                        actions[i] = "MX" + string.Join("", details) + "XM";
                        break;
                    case "WATER":
                        details[0] = "";
                        actions[i] = "WT" + string.Join("", details) + "TW";
                        break;
                    case "DUST":
                        details[0] = "";
                        actions[i] = "DT" + string.Join("", details) + "TD";
                        break;
                    case "FIRE":
                        details[0] = "";
                        actions[i] = "FR" + string.Join("", details) + "RF";
                        break;
                }
            }
            Console.WriteLine(actions[N - 1]);
        }
    }
}
