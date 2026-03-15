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
            Console.Write("Введите количество строк: ");
            int rows = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите количество столбцов: ");
            int columns = Convert.ToInt32(Console.ReadLine());
            int[,] array = new int[rows, columns];
            int[,] stats = new int[columns, 3];
            
            //int[,] array_trans = new int[columns, rows];
            //int rows = array.GetUpperBound(0) + 1;
            //int columns = array.Length / rows;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write("Введите элемент: ");
                    array[i, j] = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine();
            }

            Console.WriteLine("\nМатрица");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    Console.Write(array[i, j] + " ");
                Console.WriteLine();
            }

            //for (int i = 0; i < rows; i++)
            //    for (int j = 0; j < columns; j++)
            //        array_trans[j, i] = array[i, j];

            //Console.WriteLine("\nТранспонированная матрица");
            //for (int j = 0; j < columns; j++)
            //{
            //    for (int i = 0; i < rows; i++)
            //        Console.Write(array_trans[j, i] + " ");
            //    Console.WriteLine();
            //}

            
            // Задача 1 Необходимо определить пары номеров столбцов,
            // состоящих из одинаковых элементов

            bool check = true;
            
            for (int j = 0; j < columns; j++)
            {
                Console.WriteLine($"\n{j} столбец");
                int sum = 0;
                int mult = 1;
                int zeroCount = 0;
                for (int i = 0; i < rows; i++)
                {
                    if (array[i, j] == 0)
                        zeroCount += 1;
                    else
                    {
                        sum += array[i, j];
                        mult *= array[i, j];
                    }
                }
                stats[j, 0] = sum;
                stats[j, 1] = mult;
                stats[j, 2] = zeroCount;
                Console.WriteLine($"Сумма элементов столбца:\t{stats[j, 0]}\n" +
                    $"Произведение элементов столбца:\t{stats[j, 1]}\n" +
                    $"Количество нулей в столбце:\t{stats[j, 2]}\n");
            }

            Console.WriteLine("\nСравнение суммы, произведения и количества " +
                "нулей столбцов матрицы");
            for (int j1 = 0; j1 < columns - 1; j1++)
            {
                Console.WriteLine($"{j1} столбец");
                for (int j2 = j1 + 1; j2 < columns; j2++)
                {
                    check = true;
                    Console.WriteLine($"|{j2} столбец сравнения|");
                    for (int i = 0; i < 3; i++)
                        if (stats[j1, i] != stats[j2, i])
                        {
                            check = false;
                            break;
                        }
                    if (check) Console.WriteLine($"Пара номеров столбцов, " +
                        $"состоящих из одинаковых элементов: ({j1}, {j2})");
                }
            }
            
            //for (int j = 0; j < columns; j++)
            //    for (int i1 = 0; i1 < rows - 1; i1++)
            //        for (int i2 = i1 + 1; i2 < rows; i2++)
            //           if (array_trans[j, i1] > array_trans[j, i2])
            //            {
            //                int temp = array_trans[j, i1];
            //                array_trans[j, i1] = array_trans[j, i2];
            //                array_trans[j, i2] = temp;
            //            }

            //Console.WriteLine("\nОтсортированная транспонированная матрица");
            //for (int j = 0; j < columns; j++)
            //{
            //    for (int i = 0; i < rows; i++)
            //        Console.Write(array_trans[j, i] + " ");
            //    Console.WriteLine();
            //}

            //Console.WriteLine("\nСравнение строк транспонированной матрицы");
            //for (int j1 = 0; j1 < columns - 1; j1++)
            //{
            //    Console.WriteLine($"{j1} столбец");
            //    for (int j2 = j1 + 1; j2 < columns; j2++)
            //    {
            //        check = true;
            //        Console.WriteLine($"|{j2} столбец сравнения|");
            //        for (int i = 0; i < rows; i++)
            //        {
            //            Console.WriteLine($"||{i} строка||");
            //            Console.WriteLine($"{array_trans[j1, i]} {array_trans[j2, i]}\n");
            //            if (array_trans[j1, i] != array_trans[j2, i])
            //           {
            //                check = false;
            //            }
            //        }
            //        if (check) Console.WriteLine($"Пара номеров столбцов, " +
            //           $"состоящих из одинаковых элементов: ({j1}, {j2})");
            //    }
            //}

            
            // Задача 2 Необходимо отсортировать строки
            // по убыванию количества нулевых элементов в строках
            
            for (int i = 0; i < rows - 1; i++)
                for (int j = 0; j < rows - i - 1; j++)
                {
                    int zeroCount1 = 0;
                    int zeroCount2 = 0;

                    for (int k = 0; k < columns; k++)
                    {
                        if (array[j, k] == 0)
                            zeroCount1++;
                        if (array[j + 1, k] == 0)
                            zeroCount2++;
                    }

                    if (zeroCount1 < zeroCount2)
                    {
                        for (int k = 0; k < columns; k++)
                        {
                            int temp = array[j, k];
                            array[j, k] = array[j + 1, k];
                            array[j + 1, k] = temp;
                        }
                    }
                }

            Console.WriteLine("\nОтсортированная по нулям матрица");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    Console.Write(array[i, j] + " ");
                Console.WriteLine();
            }

            
            // Задача 3 Необходимо в массиве поменять местами
            // максимальный и минимальный элемент массива
            // (максимум и минимум - единственны)
            
            int max = int.MinValue;
            int min = int.MaxValue;
            int max_i = 0;
            int max_j = 0;
            int min_i = 0;
            int min_j = 0;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    if (array[i, j] > max)
                    {
                        max = array[i, j];
                        max_i = i;
                        max_j = j;
                    }
                    if (array[i, j] < min)
                    {
                        min = array[i, j];
                        min_i = i;
                        min_j = j;
                    }
                }
            
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    if (i == max_i && j == max_j) array[i, j] = min;
                    if (i == min_i && j == min_j) array[i, j] = max;
                }

            Console.WriteLine("\nМатрица, с замёнными " +
                "максимальным и минимальным значениями");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    Console.Write(array[i, j] + " ");
                Console.WriteLine();
            }
        }
    }
}
