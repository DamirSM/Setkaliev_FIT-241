using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Строки 2
            
            // Задача 1
            // На вход подаётся строка, состоящая из заглавных букв латинского алфавита.
            // Необходимо определить максимальную длину подстроки,
            // состоящую из последовательности элементов x, y, z (в том порядке, в котором перечислили)
            // При этом допускается неполная комбинация x; x, y
            
            string str = Console.ReadLine().ToUpper();
            int max_len = 0;
            int len = 0;
            char expected_chr = 'X';
            for (int i = 0; i < str.Length; i++)
            {
                Console.WriteLine(expected_chr);
                if (str[i] == expected_chr) len++;
                else
                {
                    expected_chr = 'Z';
                    if (max_len < len) max_len = len;
                    len = 0;
                }
                switch (expected_chr)
                {
                    case 'X':
                        expected_chr = 'Y';
                        break;
                    case 'Y':
                        expected_chr = 'Z';
                        break;
                    case 'Z':
                        expected_chr = 'X';
                        break;
                }
            }
            if (max_len < len) max_len = len;
            Console.WriteLine("Максимальная длина подстроки, состоящей из " +
                "последовательности элементов x, y, z: " + max_len);

            // Задача 2
            // Дана строка, состоящая из латинских букв.
            // Необходимо определить символ, который реже всего встречается в образце
            // a* b(A* B, A* b, a* B), где звёздочка - искомый символ
            // (символ обязательно должен присутствовать, но просто в наименьшем количестве)
            // Если таких символ несколько, то выдать все.

            int min_count = str.Length;
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string str_copy = str;

            foreach (char chr in alphabet)
            {
                string pattern = "A" + chr + "B";
                int count = 0;
                while (str_copy.Contains(pattern))
                {
                    str_copy = str_copy.Remove(str_copy.IndexOf(pattern), pattern.Length);
                    count++;
                }
                if (min_count > count && count > 0) min_count = count;
            }

            str_copy = str;
            foreach (char chr in alphabet)
            {
                string pattern = "A" + chr + "B";
                int count = 0;
                while (str_copy.Contains(pattern))
                {
                    str_copy = str_copy.Remove(str_copy.IndexOf(pattern), pattern.Length);
                    count++;
                }
                if (count == min_count) Console.WriteLine("Символ, который реже всего " +
                    "встречается в образце a * b (A * B, A * b, a * B), " +
                    "где звёздочка - искомый символ: " + chr);
            }
        }
    }
}
