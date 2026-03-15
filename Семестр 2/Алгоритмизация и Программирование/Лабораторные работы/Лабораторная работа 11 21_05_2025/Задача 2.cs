using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        unsafe static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность массива: ");
            int size = Convert.ToInt32(Console.ReadLine());

            int* nums = stackalloc int[size];
           
            //nums[0] = 312412;
            //nums[1] = 10201;
            //nums[2] = 4534354;
            //nums[3] = 123124;
            //nums[4] = 111;

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("Введите целое число: ");
                nums[i] = Convert.ToInt32(Console.ReadLine());
            }

            for (int i = 0; i < size; i++)
            {
                if (nums[i] > 0)
                {
                    int reverse = 0;
                    int temp = nums[i];

                    while (temp != 0)
                    {
                        reverse = reverse * 10 + temp % 10;
                        temp /= 10;
                    }
                    if (nums[i] == reverse)
                        Console.WriteLine("Число {0} является палиндромом", nums[i]);
                }
            }
        }
    }
}
