using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_1_setkaliev_20_09_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Обмен значений двух переменных без использования третьей
            Console.Write("Введите переменную 1: ");
            int var1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите переменную 2: ");
            int var2 = Convert.ToInt32(Console.ReadLine());

            var1 = var1 + var2;
            var2 = var1 - var2;
            var1 = var1 - var2;

            Console.WriteLine($"Переменная 1 - {var1}\nПеременная 2 - {var2}");
        }
    }
}
