using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_1_setkaliev_18_10_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Запись числа в обратном порядке, исключая нечётные цифры
            Console.Write("Запись положительного числа в обратном порядке, " +
                "исключая нечётные цифры\nВведите число: ");
            int num = Convert.ToInt32(Console.ReadLine());

            while (num > 0)
            {
                int temp = num;
                int new_num = 0;
                bool check = false;

                Console.WriteLine("temp\tdig\tnew_num");
                while (temp != 0)
                {
                    int dig = temp % 10;
                    if (dig % 2 == 0)
                    {
                        check = true;
                        new_num = new_num * 10 + dig;
                    }
                    Console.WriteLine($"{temp}\t{dig}\t{new_num}");
                    temp /= 10;

                }
                if (check) Console.WriteLine($"Изначальное число: {num}\nНовое число: {new_num}\n");
                else Console.WriteLine("В числе отсутствуют чётные цифры\n");

                Console.Write("Введите число: ");
                num = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
