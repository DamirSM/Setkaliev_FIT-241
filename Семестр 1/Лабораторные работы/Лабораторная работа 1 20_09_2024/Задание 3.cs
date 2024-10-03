using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_3_setkaliev_20_09_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Вычисление цикличного пути при помощи формул арифметической прогресии
            // Входные данные для проверки: l = 3, m = 3, p = 5
            // Результаты: n = 1 - 22, n = 2 - 50, n = 3 - 84, n = 4 - 124, n = 5 - 490
            Console.Write("Введите длину грядки: ");
            int l = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите ширину грядки: ");
            int m = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите расстояние до колодца: ");
            int p = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите количество грядок: ");
            int n = Convert.ToInt32(Console.ReadLine());
            int sum;

            sum = 2 * n * (p + m) + (((2 * 2 * l + (n - 1) * 2 * l) / 2) * n);

            Console.WriteLine($"Длина пути фермера: {sum}");
        }
    }
}
