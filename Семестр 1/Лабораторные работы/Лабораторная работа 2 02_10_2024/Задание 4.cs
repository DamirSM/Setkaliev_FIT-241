using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_4_setkaliev_02_10_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Нахождение количества элементов, оканчивающихся на "5", в последовательности n элементов
            Console.Write("Нахождение количества элементов, оканчивающихся на \"5\", в последовательности n элементов\n" +
                "Введите количество элементов: ");
            int n = Convert.ToInt32(Console.ReadLine());
            int count = 0;

            for (int i = 0; i < n; i++)
            {
                Console.Write("Введите элемент: ");
                int num = Convert.ToInt32(Console.ReadLine());

                if ((num % 5 == 0) && (num % 2 != 0)) count++;
            }

            Console.WriteLine("Количество элементов, оканчивающихся на \"5\": " + count);
        }
    }
}
