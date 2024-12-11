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
            // Строки. Только латинские буквы. "a" и "A" одинаковы. Места в слове нумеруются с 1, а не с 0.
            // На вход подаётся строка, состоящая из слов, между которыми от 1 и более пробелов.
            Console.Write("Введите строку: ");
            string str = Console.ReadLine();

            // Задание 1 Необходимо отформатировать строку таким образом,
            // чтобы между словами было по 1 пробелу.
            // (можно через Split, можно через удаление пробелов через цикл while)
            Console.Write("\nСтрока без лишних пробелов между словами: ");
            while (str.Contains("  ")) str = str.Replace("  ", " ");
            Console.WriteLine(str);

            // Задание 2 Необходимо определить кол-во слов, в которых на чётных местах стоят гласные буквы.
            string[] arr_str = str.Split(' ');
            int count = 0;
            foreach (string word in arr_str)
            {
                bool check = false;
                for (int i = 1; i < word.Length; i += 2)
                {
                    if ("aeiouy".Contains(word[i])) check = true;
                    else check = false;
                }
                if (check == true) count++;
            }
            Console.Write("\nКоличество слов, в которых на чётных местах стоят гласные буквы: ");
            Console.WriteLine(count);

            // Задание 3 Определить кол-во слов, длина которых нечётная, а первый и последний символ совпадают.
            count = 0;
            foreach (string word in arr_str)
            {
                int len = word.Length;
                if (len % 2 == 1 && word[0] == word[len - 1]) count++; 
            }
            Console.Write("\nКоличество слов, длина которых нечётная, а первый и последний символ совпадают: ");
            Console.WriteLine(count);

            // Задание 4 Выдать все слова, в которых присутствует хотя бы 1 символ "a".
            string words_a = "";
            foreach (string word in arr_str)
            {
                if ((word.ToLower()).Contains('a')) words_a += word + " ";
            }
            Console.Write("\nСлова, в которых присутствует хотя бы 1 символ \"a\": ");
            Console.WriteLine(words_a);
        }
    }
}
