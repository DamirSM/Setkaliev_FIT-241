using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_1_setkaliev_04_10_2024
{
    class Program
    {
        static void Main(string[] args)
        {
            // Определение минимального размера подпоследовательности, состоящей из единиц,
            // в последовательности из n элементов (n >=2 )
            Console.Write("Определение минимального размера подпоследовательности, " + 
		"состоящей из единиц" + 
		"\nВведите количество элементов: ");
            int n = Convert.ToInt32(Console.ReadLine());
            int count = 0;
            int mincount = int.MaxValue;

            for (int i = 0; i < n; i++)
            {
                Console.Write("Введите элемент: ");
                int num = Convert.ToInt32(Console.ReadLine());

                if (num == 1) count++;
                else
                {
                    if ((mincount > count) && (count != 0)) mincount = count;
                    count = 0;
                }
                Console.WriteLine("count: " + count + " | mincount: " + mincount);
            }

            if ((mincount > count) && (count != 0)) mincount = count;
            
            if (mincount == int.MaxValue) Console.WriteLine("В последовательности отсутствуют единицы");
            else Console.WriteLine("Минимальный размер подпоследовательности, состоящей из единиц: " + mincount);
        }
    }
}
