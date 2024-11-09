using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите длину массива: ");
            int len = Convert.ToInt32(Console.ReadLine());
            int[] array = new int[len];

            for (int i = 0; i < len; i++)
            {
                Console.Write("Введите элемент массива: ");
                array[i] = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(array[i]);
            }

            // Задача 1
            Console.WriteLine("\nЗадача 1");
            int count = 0;
            for (int i = 0; i < len; i++)
            {
                if (Math.Abs(array[i] % 10) == 3) count++;
                Console.WriteLine(array[i] % 10);
            }
            Console.WriteLine("Количество элементов, оканчивающихся на 3: " + count);

            // Задача 2
            Console.WriteLine("\nЗадача 2");
            if (array.Length > 1)
            {
                int dif = array[1] - array[0];
                bool check = true;
                if (dif <= 0) Console.WriteLine("Элементы массива не являются "
                        + "равномерно возрастающей последовательностью");
                else
                {
                    for (int i = 2; i < len; i++)
                    {
                        if (dif <= 0 || array[i] - array[i - 1] != dif) check = false;
                        dif = array[i] - array[i - 1];
                        Console.WriteLine(dif);
                    }
                    if (check) Console.WriteLine("Элементы массива являются "
                        + "равномерно возрастающей последовательностью");
                    else Console.WriteLine("Элементы массива не являются "
                        + "равномерно возрастающей последовательностью");
                }
            }
            else Console.WriteLine("Длина массива меньше 2");

            // Задача 3
            Console.WriteLine("\nЗадача 3");
            int imax = 0;
            int imin = 0;
            for (int i = 0; i < len; i++)
            {
                if (array[i] > array[imax]) imax = i;
                if (array[i] < array[imin]) imin = i;
                Console.WriteLine(array[imax] + " " + array[imin]);
            }

            Console.WriteLine("\n");
            int[] array_new = new int[len];
            for (int i = 0; i < len; i++)
            {
                array_new[i] = array[i];
                if (i == imax) array_new[i] = array[imin];
                if (i == imin) array_new[i] = array[imax];
                Console.WriteLine(array_new[i] + " " + array[i]);
            }
        }
    }
}
