using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_3_setkaliev_04_10_2024
{
    class Program
    {
        static void Main(string[] args)
        {
            // Определение максимальной суммы подпоследовательности,
            // состоящей из чётных элементов,
            // последовательности из n элементов
            Console.Write("Определение максимальной суммы подпоследовательности, " +
                "состоящей из чётных элементов" +
                "\nВведите количество элементов: ");
            int n = Convert.ToInt32(Console.ReadLine());
            bool check = false;
            int sum = 0;
            int maxsum = int.MinValue;

            for (int i = 0; i < n; i++)
            {
                Console.Write("Введите элемент: ");
                int num = Convert.ToInt32(Console.ReadLine());

                if (num % 2 == 0)
                {
                    check = true;
                    sum += num;
                }
                else
                {
                    if ((maxsum < sum) && (check)) maxsum = sum;
                    sum = 0;
                }
                Console.WriteLine("sum: " + sum + " | maxsum: " + maxsum);
            }

            if ((maxsum < sum) && (check)) maxsum = sum;

            if (!check) Console.WriteLine("В последовательности отсутствуют " +
                "чётные элементы");
            else Console.WriteLine("Максимальная сумма элементов подпоследовательности, " +
                "состоящей из чётных элементов: " + maxsum);
        }
    }
}
