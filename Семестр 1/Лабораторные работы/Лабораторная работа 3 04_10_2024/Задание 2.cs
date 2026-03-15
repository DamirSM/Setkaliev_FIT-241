using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_2_setkaliev_04_10_2024
{
    class Program
    {
        static void Main(string[] args)
        {
            // Определение максимального размера подпоследовательности,
            // состоящей из одинаковых чётных элементов,
            // последовательности из n элементов
            Console.Write("Определение максимального размера подпоследовательности, " +
                "состоящей из одинаковых чётных элементов" + 
                "\nВведите количество элементов: ");
            int n = Convert.ToInt32(Console.ReadLine());
            int count = 1;
            int maxcount = int.MinValue;
            Console.Write("Введите элемент: ");
            int prevnum = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i < n; i++)
            {
                Console.Write("Введите элемент: ");
                int num = Convert.ToInt32(Console.ReadLine());

                if ((num == prevnum) && (num % 2 == 0)) count++;
                else
                {
                    if ((maxcount < count) && (count != 1)) maxcount = count;
                    count = 1;
                }
                Console.WriteLine("count: " + count + " | maxcount: " + maxcount);

                prevnum = num;
            }

            if ((maxcount < count) && (count != 0)) maxcount = count;

            if (maxcount == int.MinValue) Console.WriteLine("В последовательности отсутствуют " +
                "подпоследовательности одинаковых чётных элементов");
            else Console.WriteLine("Максимальный размер подпоследовательности, " +
                "состоящей из одинаковых чётных элементов: " + maxcount);
        }
    }
}
