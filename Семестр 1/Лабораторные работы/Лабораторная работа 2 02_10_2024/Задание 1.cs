using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_1_setkaliev_02_10_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Нахождение количества локальных максимумов
            // (точек изменения знака производной функции с "+" на "-",
            // то есть изменения состояния функции из возрастания в убывание)
            // последовательности n элементов
            Console.Write("Нахождение количества локальных максимумов последовательности n элементов\n" +
                "Введите количество элементов последовательности: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите элемент: ");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите элемент: ");
            int num2 = Convert.ToInt32(Console.ReadLine());
            int num3;
            int max = int.MinValue;
            int count = 0;

            for (int i = 2; i < n; i++)
            {
                Console.Write("Введите элемент: ");
                num3 = Convert.ToInt32(Console.ReadLine());

                if ((num1 < num2) && (num3 < num2))
                {
                    max = num2;
                    count += 1;
                }

                Console.WriteLine($"num1: {num1} | num2: {num2} | num3: {num3} \nmax: {max} count: {count}");
                num1 = num2;
                num2 = num3;
            }

            Console.WriteLine("Количество локальных максимумов: " + count);
        }
    }
}
