using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_2_setkaliev_02_10_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Определение наличия чётности всех чисел в последовательности n элементов
            Console.Write("Определение наличия чётности всех чисел в последовательности n элементов\n" +
                "Введите количество элементов последовательности: ");
            int n = Convert.ToInt32(Console.ReadLine());
            bool check = true;

            for (int i = 0; i < n; i++)
            {
                Console.Write("Введите элемент: ");
                int num = Convert.ToInt32(Console.ReadLine());

                if (num % 2 != 0) check = false;
                Console.WriteLine(check);
            }

            if (check) Console.WriteLine("В последовательности все элементы чётные");
            else Console.WriteLine("В последовательности имеются нечётные элементы");

            //switch (check)
            //{
            //    case true:
            //        Console.WriteLine("В последовательности отсутствуют нечётные элементы");
            //        break;
            //    case false:
            //        Console.WriteLine("В последовательности имеются нечётные элементы");
            //        break;
            //}
        }
    }
}
