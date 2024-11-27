using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Fill(int N, int[][]array)
        {
            Console.Write("Введите количество элементов одномерного массива: ");
            int n = Convert.ToInt32(Console.ReadLine());
            array[N] = new int[n];

            for (int j = 0; j < n; j++)
                array[N][j] = Convert.ToInt32(Console.ReadLine());
            for (int j = 0; j < n; j++)
                Console.Write(array[N][j] + " ");

            Console.WriteLine("\n");
        }

        static void Main(string[] args)
        {
            // Зубчатый массив - массив массивов. Заводится массив, состоящий из ссылок на массивы. Каждый элемент-массив может иметь свою размерность.
            // Необходимо создать массив, состоящий из n одномерных массивов разной длины. Заполнение каждого одномерного массива выполнить с помощью функции.
            // В каждом одномерном массиве (строке зубчатого массива) определить максимальный и минимальный элемент.
            // Функциями Max, Min нельзя пользоваться.
            // Входные данные: 1) 3 строки: 3 элемента (1 2 3) 2 элемента (2 2) 5 элементов (1 -2 -2 -3 -4)
            
            Console.Write("Введите количество одномерных массивов зубчатого массива: ");
            int N = Convert.ToInt32(Console.ReadLine());
            int[][]array = new int[N][];
            Console.WriteLine();

            for (int i = 0; i < N; i++)
                Fill(i, array);

            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("\n|Массив (строка) номер " + i + "\n");
                int max = int.MinValue;
                int min = int.MaxValue;
                
                for (int j = 0; j < array[i].GetLength(0); j++)
                {
                    int num = array[i][j];
                    if (num > max) max = num;
                    if (num < min) min = num;
                }

                Console.WriteLine("||Максимальный элемент: " + max);
                Console.WriteLine("||Минимальный элемент: " + min);
            }
        }
    }
}
