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
            bool last_check = false;
            int sum = 0;
            int maxsum = int.MinValue;

            for (int i = 0; i < n; i++)
            {
                Console.Write("Введите элемент: ");
                int num = Convert.ToInt32(Console.ReadLine());

                if (num % 2 == 0)
                {
                    last_check = true;
                    check = true;
                    sum += num;
                }
                else
                {
                    if ((maxsum < sum) && (last_check)) maxsum = sum;
                    sum = 0;
                    last_check = false;
                }
                Console.WriteLine("sum: " + sum + " | maxsum: " + maxsum);
            }

            if ((maxsum < sum) && (last_check)) maxsum = sum;

            if (!check) Console.WriteLine("В последовательности отсутствуют " +
                "чётные элементы");
            else Console.WriteLine("Максимальная сумма элементов подпоследовательности, " +
                "состоящей из чётных элементов: " + maxsum);
        }
    }
}
