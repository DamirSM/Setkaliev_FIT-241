using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_2_setkaliev_20_09_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Нахождение наибольшего и наименьшего значений, используя класс Math
            Console.Write("Введите переменную 1: ");
            int var1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите переменную 2: ");
            int var2 = Convert.ToInt32(Console.ReadLine());
            int max;
            int min;

            max = (var1 + var2 + Math.Abs(var1 - var2))/2;
            min = (var1 + var2 - Math.Abs(var1 - var2))/2;

            Console.WriteLine($"Максимальное значение - {max}\nМинимальное значение - {min}");
        }
    }
}
