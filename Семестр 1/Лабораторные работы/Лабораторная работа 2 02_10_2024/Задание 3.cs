using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_3_setkaliev_02_10_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Определение второго максимума последовательности n элементов
            Console.Write("Определение второго максимума последовательности n элементов\n" +
                "Введите количество элементов последовательности: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите элемент: ");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите элемент: ");
            int num2 = Convert.ToInt32(Console.ReadLine());
            int max1 = (num1 + num2 + Math.Abs(num1 - num2)) / 2;
            int max2 = (num1 + num2 - Math.Abs(num1 - num2)) / 2;

            for (int i = 2; i < n; i++)
            {
                Console.Write("Введите элемент: ");
                int num = Convert.ToInt32(Console.ReadLine());
                if (max1 < num)
                {
                    max2 = max1;
                    max1 = num;
                }
                if ((max2 < num) && (max1 != num)) max2 = num;
                Console.WriteLine($"num: {num} | max2: {max2} | max1: {max1}");
            }
            Console.WriteLine($"Первый максимум: {max1}, второй максимум: {max2}");
        }
    }
}
